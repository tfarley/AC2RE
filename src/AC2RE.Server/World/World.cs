using AC2RE.Definitions;
using AC2RE.Server.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AC2RE.Server {

    internal class World {

        private static readonly Position TUTORIAL_START_POS = new() {
            cell = new(0x02, 0x98, 0x01, 0x09),
            frame = new(new(59.060577f, 240.199f, -44.894524f), new(0.70710677f, 0.0f, 0.0f, 0.70710677f)),
        };

        private static readonly Position ARWIC_START_POS = new() {
            cell = new(0x75, 0xB9, 0x00, 0x31),
            frame = new(new(131.13126f, 13.535009f, 127.25996f), Quaternion.Identity),
        };

        public readonly WorldDatabase worldDb;
        public readonly ServerTime serverTime;
        private readonly PacketHandler packetHandler;
        public readonly ContentManager contentManager;

        public readonly PlayerManager playerManager;
        public readonly CharacterManager characterManager;
        public readonly WorldObjectManager objectManager;
        public readonly LandblockManager landblockManager;
        public readonly InventoryManager inventoryManager;

        private int toggleCounter = 0;

        public World(WorldDatabase worldDb, ServerTime serverTime, PacketHandler packetHandler, ContentManager contentManager) {
            this.worldDb = worldDb;
            this.serverTime = serverTime;
            this.packetHandler = packetHandler;
            this.contentManager = contentManager;
            playerManager = new(packetHandler);
            characterManager = new(this);
            objectManager = new(this);
            landblockManager = new(this);
            inventoryManager = new(this);
        }

        public void addPlayerIfNecessary(ClientConnection client, Account account) {
            if (!playerManager.exists(client.id)) {
                playerManager.add(client.id, account);
            }
        }

        public bool processMessage(ClientConnection client, INetMessage genericMsg) {
            if (!playerManager.tryGet(client.id, out Player? player)) {
                throw new InvalidDataException($"Received message from client {client} with no player.");
            }

            bool handled = true;
            switch (genericMsg.opcode) {
                case MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT: {
                        CliDatInterrogationResponseMsg msg = (CliDatInterrogationResponseMsg)genericMsg;

                        playerManager.send(player, new CliDatEndDDDMsg());
                        sendCharacters(player);

                        break;
                    }
                case MessageOpcode.CHARACTER_CREATE_EVENT: {
                        CharacterCreateMsg msg = (CharacterCreateMsg)genericMsg;

                        WorldObject characterObject = CharacterGen.createCharacterObject(objectManager, contentManager, inventoryManager, ARWIC_START_POS, msg.characterName, msg.species, msg.sex, msg.physiqueTypeValues);

                        Character character = characterManager.createWithAccountAndObject(player.account.id, characterObject.id);

                        playerManager.send(player, new CharGenVerificationMsg {
                            response = CharGenResponse.OK,
                            characterIdentity = new() {
                                id = characterObject.id,
                                name = characterObject.name!.literalValue,
                                secondsGreyedOut = 0,
                                visualDesc = characterObject.visual,
                            },
                            weenieCharGenResult = 0,
                        });

                        break;
                    }
                case MessageOpcode.Evt_Login__CharacterDeletion_ID: {
                        CharacterDeletionSMsg msg = (CharacterDeletionSMsg)genericMsg;

                        if (objectManager.tryGet(msg.characterId, out WorldObject? character)) {
                            if (characterManager.tryGetWithAccountAndObject(player.account.id, character.id, out Character? characterInfo)) {
                                characterInfo.deleted = true;
                                character.destroy();

                                playerManager.send(player, new CharacterDeletionCMsg {
                                    characterId = character.id,
                                });
                                sendCharacters(player);
                            }
                        }

                        break;
                    }
                case MessageOpcode.CHARACTER_ENTER_GAME_EVENT: {
                        CharacterEnterGameMsg msg = (CharacterEnterGameMsg)genericMsg;

                        if (!characterManager.existsWithAccountAndWorldObject(player.account.id, msg.characterId)) {
                            throw new ArgumentException($"Account {player.account.id} attempted to log in with unowned character object {msg.characterId}.");
                        }

                        player.characterId = msg.characterId;

                        if (!objectManager.tryGet(msg.characterId, out WorldObject? character)) {
                            throw new ArgumentException($"Account {player.account.id} attempted to log in with non-existent character object {msg.characterId}.");
                        }

                        playerManager.send(player, new CreatePlayerMsg {
                            id = msg.characterId,
                            regionId = 1,
                        });

                        playerManager.send(player, new PlayerDescMsg {
                            qualities = character.qualities,
                        });

                        Dictionary<uint, InventProfile> inventoryByLocationTable = new();
                        Dictionary<InstanceId, InventProfile> inventoryByIdTable = new();

                        foreach ((InvLoc equipLoc, InstanceId itemId) in character.invLocToEquippedItemIdEnumerable) {
                            if (objectManager.tryGet(itemId, out WorldObject? item)) {
                                InventProfile profile = new() {
                                    visualDescInfo = new() {
                                        scale = Vector3.One,
                                        cachedVisualDesc = item.visual,
                                    },
                                    slotsTaken = equipLoc,
                                    location = equipLoc,
                                    it = 0,
                                    id = item.id,
                                };
                                if (item.globalAppearanceModifiers != null) {
                                    profile.visualDescInfo.appInfoHash = new();
                                    foreach (var entry in item.globalAppearanceModifiers.appearanceInfos) {
                                        profile.visualDescInfo.appInfoHash[entry.Key] = entry.Value;
                                    }
                                }
                                inventoryByLocationTable[(uint)equipLoc] = profile;
                                inventoryByIdTable[item.id] = profile;

                                DataId weenieStateDid = new(0x71000000 + item.entityDid.id - DbTypeDef.TYPE_TO_DEF[DbType.ENTITYDESC].baseDid.id);
                                WState clothingWeenieState = contentManager.getWeenieState(weenieStateDid);
                                if (clothingWeenieState.package is Clothing clothing) {
                                    // TODO: contentManager.getInheritedVisualDesc(item.visual)? But it seems wrong, since the topmost parent of human starter pants is 0x1F00003E which is actually overriding skin color which doesn't make sense - not sure if that's a special override that just needs to be blocked or if inheritance isn't the correct thing to do...
                                    PartGroupDataDesc? globalAppearanceModifiers = item.globalAppearanceModifiers;
                                    if (globalAppearanceModifiers != null) {
                                        foreach ((DataId appDid, Dictionary<AppearanceKey, float> appearances) in globalAppearanceModifiers.appearanceInfos) {
                                            Dictionary<AppearanceKey, float> clonedAppearances = new(appearances);
                                            clonedAppearances[AppearanceKey.WORN] = 1.0f;
                                            character.globalAppearanceModifiers.appearanceInfos[appDid] = clonedAppearances;
                                        }
                                    }
                                }
                            }
                        }

                        List<InstanceId> contentIds = new();
                        foreach (InstanceId contentId in character.containedItemIdsEnumerable) {
                            if (!character.isEquipped(contentId)) {
                                contentIds.Add(contentId);
                            }
                        }

                        playerManager.send(player, new InterpCEventPrivateMsg {
                            netEvent = new HandleCharacterSessionStartCEvt {
                                money = 12345,
                                actRegistry = new() {
                                    viewingProtectionEffectId = 0,
                                    actSceneTable = new() {
                                        { 0x40000005, new() },
                                        { 0x40000006, new() },
                                        { 0x40000007, new() },
                                        { 0x40000008, new() },
                                        { 0x40000009, new() },
                                        { 0x4000000A, new() },
                                    }
                                },
                                quests = new() {
                                    new() {
                                        questId = QuestId.FINDEXPLORERARWIC,
                                        questName = new(new(0x250017EB), 2824895724),
                                        questDescription = new(new(0x250017EB), 1816499044),
                                        iconDid = new(0x4100034B),
                                        challengeLevel = -999,
                                        questStatus = QuestStatus.UNDERWAY,
                                        curPhase = 1,
                                        curJournalEntry = new(new(0x250017EB), 777789010),
                                        bestowalTime = 129500898.25912432,
                                        doneTime = -1355582621.7408757,
                                        expired = true,
                                        maxedOut = true,
                                        secondsRemaining = 10800,
                                        secondsUntilRetry = 0,
                                        playFxOnUpdate = false,
                                    },
                                },
                                options = new() {
                                    contentFlags =
                                        GameplayOptionsProfile.ContentFlag.SHORTCUT_ARRAY
                                        | GameplayOptionsProfile.ContentFlag.SHOW_RANGE_DAMAGE_OTHER
                                        | GameplayOptionsProfile.ContentFlag.SAVED_UI_LOCATIONS
                                        | GameplayOptionsProfile.ContentFlag.RADAR_MASK
                                        | GameplayOptionsProfile.ContentFlag.FILTER_HASH
                                        | GameplayOptionsProfile.ContentFlag.BIT_FIELD
                                        | GameplayOptionsProfile.ContentFlag.CHAT_FONT_COLORS
                                        | GameplayOptionsProfile.ContentFlag.CHAT_FONT_SIZES
                                        | GameplayOptionsProfile.ContentFlag.CHAT_POPUP_FLAGS
                                        | GameplayOptionsProfile.ContentFlag.WINDOW_TO_CHANNEL,
                                    shortcutArray = Enumerable.Repeat(new ShortcutInfo { type = ShortcutType.UNDEF }, 100).ToList(),
                                    whichShortcutSet = 1,
                                    damageTextRangeOther = 1.0f,
                                    savedUILocations = new(),
                                    /*m_savedUILocations = new() {
                                        contents = new() {
                                        { 0, new() {
                                            { 0xA05C6B95, new() { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0xA0446B95, new() { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0xA04C6B95, new() { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0xA0746B95, new() { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0x6433C3C7, new() { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0x9A25490C, new() { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0xA0F792C9, new() { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                        } }
                                    }
                                    },*/
                                    radarMask = 0xFFFFFFFF,
                                    filters = new() {
                                        { 1, TextType.ALL },
                                        { 2, TextType.GENERAL },
                                        { 3, TextType.FELLOWSHIP },
                                        { 4, TextType.ALLEGIANCE },
                                    },
                                    bitfield = (GameplayOptionsProfile.Flag)0x80024FF5,
                                    version = GameplayOptionsProfile.Version.LATEST_VERSION,
                                    chatFontColors = new() {
                                        { TextType.ERROR, 0 },
                                        { TextType.COMBAT, 1 },
                                        { TextType.ADMIN, 2 },
                                        { TextType.STANDARD, 3 },
                                        { TextType.SAY, 4 },
                                        { TextType.TELL, 5 },
                                        { TextType.EMOTE, 6 },
                                        { TextType.LOG, 4 },
                                        { TextType.BROADCAST, 9 },
                                        { TextType.FELLOWSHIP, 7 },
                                        { TextType.ALLEGIANCE, 8 },
                                        { TextType.CHAT_ENTRY, 4 },
                                        { TextType.GENERAL, 4 },
                                        { TextType.TRADE, 4 },
                                        { TextType.REGION, 4 },
                                        { TextType.FACTION, 4 },
                                        { TextType.DEVOTED, 4 },
                                        { TextType.PK, 4 },
                                    },
                                    chatFontSizes = new() {
                                        { TextType.ERROR, 0 },
                                        { TextType.COMBAT, 0 },
                                        { TextType.ADMIN, 0 },
                                        { TextType.STANDARD, 0 },
                                        { TextType.SAY, 0 },
                                        { TextType.TELL, 0 },
                                        { TextType.EMOTE, 0 },
                                        { TextType.LOG, 0 },
                                        { TextType.BROADCAST, 0 },
                                        { TextType.FELLOWSHIP, 0 },
                                        { TextType.ALLEGIANCE, 0 },
                                        { TextType.CHAT_ENTRY, 0 },
                                        { TextType.GENERAL, 0 },
                                        { TextType.TRADE, 0 },
                                        { TextType.REGION, 0 },
                                        { TextType.FACTION, 0 },
                                        { TextType.DEVOTED, 0 },
                                        { TextType.PK, 0 },
                                    },
                                    windowToChannel = new() {
                                        { 1, TextType.SAY },
                                        { 2, TextType.GENERAL },
                                        { 3, TextType.FELLOWSHIP },
                                        { 4, TextType.ALLEGIANCE },
                                    },
                                    chatPopupFlags = new() {
                                        { TextType.BROADCAST, true },
                                        { TextType.FELLOWSHIP, true },
                                        { TextType.ALLEGIANCE, true },
                                    },
                                    windowOpacities = new() {
                                        { 0xA05C6B95, 0.65f },
                                        { 0xA0446B95, 0.65f },
                                        { 0xA04C6B95, 0.65f },
                                        { 0xA0746B95, 0.65f },
                                    },
                                },
                                skills = new() {
                                    skillCredits = 0,
                                    untrainXp = 0,
                                    perkTypes = new(),
                                    typeUntrained = 0,
                                    categories = new(),
                                    skills = new() {
                                        {
                                            SkillId.HUM_ME_RIPOSTE,
                                            new() {
                                                lastUsedTime = -1,
                                                mask = 33,
                                                grantedTime = -1,
                                                skillOverride = 1,
                                                typeSkill = SkillId.HUM_ME_RIPOSTE,
                                            }
                                        },
                                        {
                                            SkillId.HUM_ME_UNPREDICTABLEBLOW,
                                            new() {
                                                lastUsedTime = -1,
                                                mask = 33,
                                                grantedTime = -1,
                                                skillOverride = 1,
                                                typeSkill = SkillId.HUM_ME_UNPREDICTABLEBLOW,
                                            }
                                        },
                                        {
                                            SkillId.COM_LIFESTONERECALL,
                                            new() {
                                                lastUsedTime = -1,
                                                mask = 33,
                                                grantedTime = -1,
                                                skillOverride = 1,
                                                typeSkill = SkillId.COM_LIFESTONERECALL,
                                            }
                                        },
                                    },
                                },
                                effectRegistry = new() {
                                    qualitiesModifiedCount = null,
                                    appliedFx = new(),
                                    baseEffectRegistry = null,
                                    effectIdCounter = 3,
                                    effectInfo = null,
                                    lastPulseTime = -1.0,
                                    equipperEffectIds = null,
                                    acquirerEffectIds = null,
                                    flags = 0x000C0001,
                                    trackedEffects = null,
                                    topEffects = null,
                                    effectCategorizationTable = null,
                                    appliedAppearances = new(),
                                },
                                filledInventoryLocations = (InvLoc)1531,
                                inventoryByLocationTable = inventoryByLocationTable,
                                inventoryByIdTable = inventoryByIdTable,
                                containerSegments = new() {
                                    new() {
                                        segmentMaxSize = 12,
                                        segmentSize = 8,
                                    },
                                    new() {
                                        segmentMaxSize = 12,
                                        segmentSize = 12,
                                    },
                                    new() {
                                        segmentMaxSize = 12,
                                        segmentSize = 11,
                                    },
                                    new() {
                                        segmentMaxSize = 42,
                                        segmentSize = 30,
                                    },
                                },
                                containerIds = new() {

                                },
                                contentIds = contentIds,
                                localFactionStatus = FactionStatus.PEACE,
                                serverFactionStatus = FactionStatus.UNDEF,
                            }
                        });

                        foreach (InstanceId itemId in character.containedItemIdsEnumerable) {
                            if (objectManager.tryGet(itemId, out WorldObject? item)) {
                                item.enterWorld();
                            }
                        }

                        character.enterWorld();

                        landblockManager.syncPlayerVisibility(player);
                        break;
                    }
                case MessageOpcode.Evt_Login__CharExitGame_ID: {
                        CharacterExitGameSMsg msg = (CharacterExitGameSMsg)genericMsg;

                        if (player.characterId == msg.characterId) {
                            player.visibleObjectIds.Clear();
                            player.characterId = InstanceId.NULL;

                            playerManager.send(player, new CharacterExitGameCMsg());
                            sendCharacters(player);
                        }

                        break;
                    }
                case MessageOpcode.CLIDAT_REQUEST_DATA_EVENT: {
                        CliDatRequestDataMsg msg = (CliDatRequestDataMsg)genericMsg;
                        playerManager.send(player, new CliDatErrorMsg {
                            qdid = msg.qdid,
                            error = 1,
                        });
                        break;
                    }
                case MessageOpcode.Evt_Interp__InterpSEvent_ID: {
                        InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                        // TODO: Just for testing - when pressing the attack mode button, toggle Refining effect UI image
                        if (msg.netEvent.funcId == ServerEventFunctionId.Combat__StartAttack) {
                            if (toggleCounter % 2 == 0) {
                                SingletonPkg<Effect> refiningEffect = new() {
                                    did = new(0x6F0011ED),
                                };
                                playerManager.send(player, new InterpCEventPrivateMsg {
                                    netEvent = new ClientAddEffectCEvt {
                                        effectRecord = new() {
                                            timeDemotedFromTopLevel = -1.0,
                                            timeCast = 129996502.8136027,
                                            casterId = player.characterId,
                                            timeout = 0.0f,
                                            appFloat = 0.0f,
                                            spellcraft = 1.0f,
                                            appInt = 0,
                                            pk = false,
                                            appPackage = null,
                                            timePromotedToTopLevel = -1.0,
                                            effect = refiningEffect,
                                            actingForWhomId = default,
                                            skillDid = default,
                                            fromItemId = player.characterId,
                                            flags = 0x00000051,
                                            durabilityLevel = 0,
                                            relatedEffectId = 0,
                                            effectId = 0x00000BD9,
                                            categories = 1,
                                            maxDurabilityLevel = 0,
                                        },
                                        effectId = 0x00000BD9,
                                    }
                                });
                            } else {
                                playerManager.send(player, new InterpCEventPrivateMsg {
                                    netEvent = new ClientRemoveEffectCEvt {
                                        effectId = 0x00000BD9,
                                    }
                                });
                            }

                            WorldObject newTestObject = objectManager.create();
                            newTestObject.visual = new() {
                                parentDid = new(0x1F000000 + (uint)toggleCounter),
                            };
                            newTestObject.physics = new() {
                                pos = new() {
                                    cell = new(0x75, 0xB9, 0x00, 0x31),
                                    frame = new(new(131.13126f - toggleCounter * 1.0f, 13.535009f + toggleCounter * 1.0f, 127.25996f), Quaternion.Identity),
                                },
                            };
                            newTestObject.qualities = new() {
                                weenieDesc = new() {
                                    packageType = PackageType.PlayerAvatar,
                                    name = new($"TestObj 0x{toggleCounter:X}"),
                                    entityDid = new(0x47000530),
                                },
                            };

                            newTestObject.enterWorld();

                            if (objectManager.tryGet(player.characterId, out WorldObject? character) && character.inWorld) {
                                character.health = toggleCounter;

                                character.doFx(FxId.PORTAL_USE, 1.0f);
                            }

                            toggleCounter++;
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Examination__QueryExaminationProfile) {
                            QueryExaminationProfileSEvt sEvent = (QueryExaminationProfileSEvt)msg.netEvent;
                            playerManager.send(player, new InterpCEventPrivateMsg {
                                netEvent = new UpdateExaminationProfileCEvt {
                                    profile = new() {
                                        request = sEvent.request,
                                        nodes = new() {
                                            new() {
                                                order = 2,
                                                type = ExaminationDataNode.DataType.INT,
                                                valInt = 12345,
                                                appearanceId = 3193660691,
                                            }
                                        }
                                    }
                                }
                            });
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Inventory__DirectiveEquipItem) {
                            DirectiveEquipItemSEvt sEvent = (DirectiveEquipItemSEvt)msg.netEvent;
                            inventoryManager.equipItem(sEvent.equipDesc, player);
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Inventory__DirectiveUnEquipItem) {
                            DirectiveUnequipItemSEvt sEvent = (DirectiveUnequipItemSEvt)msg.netEvent;
                            inventoryManager.unequipItem(sEvent.equipDesc, player);
                        }
                        break;
                    }
                case MessageOpcode.Evt_Physics__CLookAtDir_ID: {
                        CLookAtDirMsg msg = (CLookAtDirMsg)genericMsg;

                        if (objectManager.tryGet(player.characterId, out WorldObject? character) && character.inWorld) {
                            character.lookAtDir = new(msg.x, msg.z);
                        }

                        break;
                    }
                case MessageOpcode.Evt_Physics__CPosition_ID: {
                        CPositionMsg msg = (CPositionMsg)genericMsg;

                        if (objectManager.tryGet(player.characterId, out WorldObject? character) && character.inWorld) {
                            character.offset = msg.posPack.offset;
                            character.heading = msg.posPack.heading.rotDegrees;
                            character.motion = msg.posPack.doMotion;
                            character.jumped = msg.posPack.packFlags.HasFlag(CPositionPack.PackFlag.JUMP);
                            character.impulseVel = msg.posPack.jumpVel;
                        }

                        break;
                    }
                default: {
                        Logs.NET.error("Unhandled opcode - message not processed!",
                            "op", genericMsg.opcode);
                        handled = false;
                        break;
                    }
            }
            return handled;
        }

        private void sendCharacters(Player player) {
            List<CharacterIdentity> characterIdentities = new();
            foreach (Character characterInfo in characterManager.getWithAccount(player.account.id)) {
                if (objectManager.tryGet(characterInfo.objectId, out WorldObject? character)) {
                    characterIdentities.Add(new() {
                        id = character.id,
                        name = character.name!.literalValue,
                        secondsGreyedOut = 0,
                        visualDesc = character.visual,
                    });
                }
            }

            List<string> characterNames = new();
            List<InstanceId> characterIds = new();
            foreach (CharacterIdentity characterIdentity in characterIdentities) {
                characterNames.Add(characterIdentity.name);
                characterIds.Add(characterIdentity.id);
            }

            playerManager.send(player, new MinCharSetMsg {
                numAllowedCharacters = 0,
                accountName = player.account.userName,
                characterNames = characterNames,
                characterIds = characterIds,
            });

            playerManager.send(player, new CharacterSetMsg {
                characters = characterIdentities,
                deletedCharacters = null,
                status = 0,
                numAllowedCharacters = 10,
                accountName = player.account.userName,
                unk1 = 1,
                hasLegions = true,
                useTurbineChat = true,
            });
        }

        public void tick() {
            packetHandler.processNetBlobs(this);

            landblockManager.update();

            objectManager.broadcastUpdates();
        }

        public void save() {
            WorldSave worldSave = new();
            characterManager.contributeToSave(worldSave);
            objectManager.contributeToSave(worldSave);
            worldDb.save(worldSave);
        }

        public void disconnectAll() {
            playerManager.disconnectAll();
        }
    }
}
