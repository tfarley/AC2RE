using AC2E.Def;
using AC2E.Server.Database;
using AC2E.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AC2E.Server {

    internal class World {

        public const float DEG_TO_RAG = MathF.PI / 180.0f;
        public const float RAD_TO_DEG = 180.0f / MathF.PI;

        private static readonly Position TUTORIAL_START_POS = new Position {
            cell = new CellId(0x02, 0x98, 0x01, 0x09),
            frame = new Frame(new Vector3(59.060577f, 240.199f, -44.894524f), new Quaternion(0.70710677f, 0.0f, 0.0f, 0.70710677f)),
        };

        private static readonly Position ARWIC_START_POS = new Position {
            cell = new CellId(0x75, 0xB9, 0x00, 0x31),
            frame = new Frame(new Vector3(131.13126f, 13.535009f, 127.25996f), Quaternion.Identity),
        };

        private readonly WorldDatabase worldDb;
        private readonly ServerTime serverTime;
        private readonly PacketHandler packetHandler;
        private readonly ContentManager contentManager;

        private readonly PlayerManager playerManager;
        private readonly CharacterManager characterManager;
        private readonly WorldObjectManager objectManager;
        private readonly InventoryManager inventoryManager;

        private int toggleCounter = 0;

        public World(WorldDatabase worldDb, ServerTime serverTime, PacketHandler packetHandler, ContentManager contentManager) {
            this.worldDb = worldDb;
            this.serverTime = serverTime;
            this.packetHandler = packetHandler;
            this.contentManager = contentManager;
            playerManager = new PlayerManager(packetHandler);
            characterManager = new CharacterManager(worldDb);
            objectManager = new WorldObjectManager(worldDb, packetHandler, playerManager);
            inventoryManager = new InventoryManager(packetHandler, playerManager, objectManager);

            objectManager.enterWorld(objectManager.getAllInWorld());
        }

        public void addPlayerIfNecessary(ClientId clientId, Account account) {
            if (!playerManager.exists(clientId)) {
                playerManager.add(clientId, account);
            }
        }

        public bool processMessage(ClientId clientId, INetMessage genericMsg) {
            Player player = playerManager.get(clientId);

            bool handled = true;
            switch (genericMsg.opcode) {
                case MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT: {
                        CliDatInterrogationResponseMsg msg = (CliDatInterrogationResponseMsg)genericMsg;

                        packetHandler.send(player.clientId, new CliDatEndDDDMsg());
                        sendCharacters(player);

                        break;
                    }
                case MessageOpcode.CHARACTER_CREATE_EVENT: {
                        CharacterCreateMsg msg = (CharacterCreateMsg)genericMsg;

                        WorldObject characterObject = CharacterGen.createCharacterObject(objectManager, contentManager, inventoryManager, ARWIC_START_POS, msg.characterName, msg.species, msg.sex, msg.physiqueTypeValues);

                        Character character = characterManager.createWithAccountAndWorldObject(player.account.id, characterObject.id);

                        packetHandler.send(player.clientId, new CharGenVerificationMsg {
                            response = CharGenResponse.OK,
                            characterIdentity = new CharacterIdentity {
                                id = characterObject.id,
                                name = characterObject.qualities.weenieDesc.name.literalValue,
                                secondsGreyedOut = 0,
                                visualDesc = characterObject.visual,
                            },
                            weenieCharGenResult = 0,
                        });

                        break;
                    }
                case MessageOpcode.Evt_Login__CharacterDeletion_ID: {
                        CharacterDeletionSMsg msg = (CharacterDeletionSMsg)genericMsg;

                        WorldObject characterObject = objectManager.get(msg.characterId);
                        Character character = characterManager.getWithAccountAndWorldObject(player.account.id, characterObject.id);

                        character.deleted = true;
                        characterObject.deleted = true;

                        packetHandler.send(player.clientId, new CharacterDeletionCMsg {
                            characterId = characterObject.id,
                        });
                        sendCharacters(player);

                        break;
                    }
                case MessageOpcode.CHARACTER_ENTER_GAME_EVENT: {
                        CharacterEnterGameMsg msg = (CharacterEnterGameMsg)genericMsg;

                        if (!characterManager.existsWithAccountAndWorldObject(player.account.id, msg.characterId)) {
                            throw new ArgumentException($"Account {player.account.id} attempted to log in with unowned character object {msg.characterId}.");
                        }

                        player.characterId = msg.characterId;

                        WorldObject character = objectManager.get(msg.characterId);

                        packetHandler.send(player.clientId, new CreatePlayerMsg {
                            id = msg.characterId,
                            regionId = 1,
                        });

                        packetHandler.send(player.clientId, new PlayerDescMsg {
                            qualities = character.qualities,
                        });

                        Dictionary<uint, InventProfile> inventoryByLocationTable = new Dictionary<uint, InventProfile>();
                        Dictionary<InstanceId, InventProfile> inventoryByIdTable = new Dictionary<InstanceId, InventProfile>();

                        List<WorldObject> playerInventory = objectManager.getAllInContainer(character.id);
                        foreach ((InvLoc equipLoc, InstanceId itemId) in character.equippedItems) {
                            WorldObject item = objectManager.get(itemId);
                            InventProfile profile = new InventProfile {
                                visualDescInfo = new VisualDescInfo {
                                    scale = Vector3.One,
                                    cachedVisualDesc = item.visual,
                                },
                                slotsTaken = equipLoc,
                                location = equipLoc,
                                it = 0,
                                id = item.id,
                            };
                            if (item.visual.globalAppearanceModifiers != null) {
                                profile.visualDescInfo.appInfoHash = new AppInfoHash();
                                foreach (var entry in item.visual.globalAppearanceModifiers.appearanceInfos) {
                                    profile.visualDescInfo.appInfoHash[entry.Key] = entry.Value;
                                }
                            }
                            inventoryByLocationTable[(uint)equipLoc] = profile;
                            inventoryByIdTable[item.id] = profile;

                            DataId weenieStateDid = new DataId(0x71000000 + item.qualities.weenieDesc.entityDid.id - DbTypeDef.TYPE_TO_DEF[DbType.ENTITYDESC].baseDid.id);
                            WState clothingWeenieState = contentManager.getWeenieState(weenieStateDid);
                            Clothing clothing = clothingWeenieState.package as Clothing;
                            if (clothing != null) {
                                // TODO: contentManager.getInheritedVisualDesc(item.visual)? But it seems wrong, since the topmost parent of human starter pants is 0x1F00003E which is actually overriding skin color which doesn't make sense - not sure if that's a special override that just needs to be blocked or if inheritance isn't the correct thing to do...
                                foreach ((DataId appDid, Dictionary<AppearanceKey, float> appearances) in item.visual.globalAppearanceModifiers.appearanceInfos) {
                                    Dictionary<AppearanceKey, float> clonedAppearances = new Dictionary<AppearanceKey, float>(appearances);
                                    clonedAppearances[AppearanceKey.WORN] = 1.0f;
                                    character.visual.globalAppearanceModifiers.appearanceInfos[appDid] = clonedAppearances;
                                }
                            }
                        }

                        packetHandler.send(player.clientId, new InterpCEventPrivateMsg {
                            netEvent = new HandleCharacterSessionStartCEvt {
                                money = 12345,
                                actRegistry = new ActRegistry {
                                    viewingProtectionEffectId = 0,
                                    actSceneTable = new Dictionary<uint, List<uint>> {
                                        { 0x40000005, new List<uint>() },
                                        { 0x40000006, new List<uint>() },
                                        { 0x40000007, new List<uint>() },
                                        { 0x40000008, new List<uint>() },
                                        { 0x40000009, new List<uint>() },
                                        { 0x4000000A, new List<uint>() },
                                    }
                                },
                                quests = new GMQuestInfoList {
                                    new GMQuestInfo {
                                        questId = QuestId.QUESTFINDEXPLORERARWIC,
                                        questName = new StringInfo(new DataId(0x250017EB), 2824895724),
                                        questDescription = new StringInfo(new DataId(0x250017EB), 1816499044),
                                        iconDid = new DataId(0x4100034B),
                                        challengeLevel = -999,
                                        questStatus = QuestStatus.UNDERWAY,
                                        curPhase = 1,
                                        curJournalEntry = new StringInfo(new DataId(0x250017EB), 777789010),
                                        bestowalTime = 129500898.25912432,
                                        doneTime = -1355582621.7408757,
                                        expired = true,
                                        maxedOut = true,
                                        secondsRemaining = 10800,
                                        secondsUntilRetry = 0,
                                        playFxOnUpdate = false,
                                    },
                                },
                                options = new GameplayOptionsProfile {
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
                                    savedUILocations = new UISaveLocations(),
                                    /*m_savedUILocations = new UISaveLocationsPkg {
                                        contents = new Dictionary<uint, Dictionary<uint, UISaveLocationsPkg.UILocationData>> {
                                        { 0, new Dictionary<uint, UISaveLocationsPkg.UILocationData> {
                                            { 0xA05C6B95, new UISaveLocationsPkg.UILocationData { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0xA0446B95, new UISaveLocationsPkg.UILocationData { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0xA04C6B95, new UISaveLocationsPkg.UILocationData { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0xA0746B95, new UISaveLocationsPkg.UILocationData { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0x6433C3C7, new UISaveLocationsPkg.UILocationData { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0x9A25490C, new UISaveLocationsPkg.UILocationData { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                            { 0xA0F792C9, new UISaveLocationsPkg.UILocationData { m_x0 = -1.00125f, m_y0 = 0.7f, m_x1 = -0.01125f, m_y1 = 0.4f, m_shown = true, } },
                                        } }
                                    }
                                    },*/
                                    radarMask = 0xFFFFFFFF,
                                    filters = new Dictionary<uint, TextType> {
                                        { 1, TextType.ALL },
                                        { 2, TextType.GENERAL },
                                        { 3, TextType.FELLOWSHIP },
                                        { 4, TextType.ALLEGIANCE },
                                    },
                                    bitfield = (GameplayOptionsProfile.Flag)0x80024FF5,
                                    version = GameplayOptionsProfile.Version.LATEST_VERSION,
                                    chatFontColors = new Dictionary<TextType, uint> {
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
                                    chatFontSizes = new Dictionary<TextType, uint> {
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
                                    windowToChannel = new Dictionary<uint, TextType> {
                                        { 1, TextType.SAY },
                                        { 2, TextType.GENERAL },
                                        { 3, TextType.FELLOWSHIP },
                                        { 4, TextType.ALLEGIANCE },
                                    },
                                    chatPopupFlags = new Dictionary<TextType, bool> {
                                        { TextType.BROADCAST, true },
                                        { TextType.FELLOWSHIP, true },
                                        { TextType.ALLEGIANCE, true },
                                    },
                                    windowOpacities = new Dictionary<uint, float> {
                                        { 0xA05C6B95, 0.65f },
                                        { 0xA0446B95, 0.65f },
                                        { 0xA04C6B95, 0.65f },
                                        { 0xA0746B95, 0.65f },
                                    },
                                },
                                skills = new SkillRepository {
                                    skillCredits = 0,
                                    untrainXp = 0,
                                    perkTypes = new Dictionary<uint, uint>(),
                                    typeUntrained = 0,
                                    categories = new Dictionary<uint, uint>(),
                                    skills = new Dictionary<SkillId, SkillInfo> {
                                        { SkillId.HUM_ME_RIPOSTE, new SkillInfo {
                                            lastUsedTime = -1,
                                            mask = 33,
                                            grantedTime = -1,
                                            skillOverride = 1,
                                            typeSkill = SkillId.HUM_ME_RIPOSTE,
                                        } },
                                        { SkillId.HUM_ME_UNPREDICTABLEBLOW, new SkillInfo {
                                            lastUsedTime = -1,
                                            mask = 33,
                                            grantedTime = -1,
                                            skillOverride = 1,
                                            typeSkill = SkillId.HUM_ME_UNPREDICTABLEBLOW,
                                        } },
                                        { SkillId.COM_LIFESTONERECALL, new SkillInfo {
                                            lastUsedTime = -1,
                                            mask = 33,
                                            grantedTime = -1,
                                            skillOverride = 1,
                                            typeSkill = SkillId.COM_LIFESTONERECALL,
                                        } },
                                    },
                                },
                                effectRegistry = new EffectRegistry {
                                    qualitiesModifiedCount = null,
                                    appliedFx = new Dictionary<uint, uint>(),
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
                                    appliedAppearances = new Dictionary<uint, uint>(),
                                },
                                filledInventoryLocations = (InvLoc)1531,
                                inventoryByLocationTable = inventoryByLocationTable,
                                inventoryByIdTable = inventoryByIdTable,
                                containerSegments = new List<ContainerSegmentDescriptor> {
                                    new ContainerSegmentDescriptor {
                                        segmentMaxSize = 12,
                                        segmentSize = 8,
                                    },
                                    new ContainerSegmentDescriptor {
                                        segmentMaxSize = 12,
                                        segmentSize = 12,
                                    },
                                    new ContainerSegmentDescriptor {
                                        segmentMaxSize = 12,
                                        segmentSize = 11,
                                    },
                                    new ContainerSegmentDescriptor {
                                        segmentMaxSize = 42,
                                        segmentSize = 30,
                                    },
                                },
                                containerIds = new List<InstanceId> {

                                },
                                contentIds = character.containedItems,
                                localFactionStatus = FactionStatus.PEACE,
                                serverFactionStatus = FactionStatus.UNDEF,
                            }
                        });

                        objectManager.syncNewClient(player.clientId);

                        objectManager.enterWorld(playerInventory);

                        objectManager.enterWorld(character);
                        break;
                    }
                case MessageOpcode.Evt_Login__CharExitGame_ID: {
                        CharacterExitGameSMsg msg = (CharacterExitGameSMsg)genericMsg;

                        if (player.characterId == msg.characterId) {
                            packetHandler.send(player.clientId, new CharacterExitGameCMsg());
                            sendCharacters(player);
                        }

                        break;
                    }
                case MessageOpcode.CLIDAT_REQUEST_DATA_EVENT: {
                        CliDatRequestDataMsg msg = (CliDatRequestDataMsg)genericMsg;
                        packetHandler.send(player.clientId, new CliDatErrorMsg {
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
                                SingletonPkg<Effect> refiningEffect = new SingletonPkg<Effect> {
                                    did = new DataId(0x6F0011ED),
                                };
                                packetHandler.send(player.clientId, new InterpCEventPrivateMsg {
                                    netEvent = new ClientAddEffectCEvt {
                                        effectRecord = new EffectRecord {
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
                                packetHandler.send(player.clientId, new InterpCEventPrivateMsg {
                                    netEvent = new ClientRemoveEffectCEvt {
                                        effectId = 0x00000BD9,
                                    }
                                });
                            }

                            WorldObject newTestObject = objectManager.create();
                            newTestObject.visual = new VisualDesc {
                                packFlags = VisualDesc.PackFlag.PARENT,
                                parentDid = new DataId(0x1F000000 + (uint)toggleCounter),
                            };
                            newTestObject.physics = new PhysicsDesc {
                                packFlags = PhysicsDesc.PackFlag.POSITION,
                                pos = new Position {
                                    cell = new CellId(0x75, 0xB9, 0x00, 0x31),
                                    frame = new Frame(new Vector3(131.13126f - toggleCounter * 1.0f, 13.535009f + toggleCounter * 1.0f, 127.25996f), Quaternion.Identity),
                                },
                            };
                            newTestObject.qualities = new CBaseQualities {
                                weenieDesc = new WeenieDesc {
                                    packFlags = WeenieDesc.PackFlag.MY_PACKAGE_ID | WeenieDesc.PackFlag.NAME | WeenieDesc.PackFlag.ENTITY_DID,
                                    packageType = PackageType.PlayerAvatar,
                                    name = new StringInfo($"TestObj 0x{toggleCounter:X}"),
                                    entityDid = new DataId(0x47000530),
                                },
                            };

                            objectManager.enterWorld(newTestObject);

                            packetHandler.send(player.clientId, new QualUpdateIntPrivateMsg {
                                type = IntStat.HEALTH_CURRENTLEVEL, // TODO: Health_CurrentLevel_IntStat
                                value = toggleCounter,
                            });

                            WorldObject character = objectManager.get(player.characterId);
                            if (character != null && character.inWorld) {
                                packetHandler.send(player.clientId, new DoFxMsg {
                                    senderIdWithStamp = character.getInstanceIdWithStamp(character.physics.visualOrderStamp),
                                    fxId = FxId.PORTAL_USE,
                                    scalar = 1.0f,
                                });

                                character.physics.visualOrderStamp++;
                            }

                            toggleCounter++;
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Examination__QueryExaminationProfile) {
                            QueryExaminationProfileSEvt sEvent = (QueryExaminationProfileSEvt)msg.netEvent;
                            packetHandler.send(player.clientId, new InterpCEventPrivateMsg {
                                netEvent = new UpdateExaminationProfileCEvt {
                                    profile = new ExaminationProfile {
                                        request = sEvent.request,
                                        nodes = new List<ExaminationDataNode> {
                                            new ExaminationDataNode {
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

                        WorldObject character = objectManager.get(player.characterId);
                        if (character != null && character.inWorld) {
                            character.physics.headingX = msg.x;
                            character.physics.headingZ = msg.z;

                            playerManager.broadcastSend(player.clientId, new LookAtDirMsg {
                                senderIdWithStamp = character.getInstanceIdWithStamp(),
                                x = character.physics.headingX,
                                z = character.physics.headingZ,
                            });
                        }

                        break;
                    }
                case MessageOpcode.Evt_Physics__CPosition_ID: {
                        CPositionMsg msg = (CPositionMsg)genericMsg;

                        WorldObject character = objectManager.get(player.characterId);
                        if (character != null && character.inWorld) {
                            character.heading = msg.pos.heading.rotDegrees;
                            character.motion = msg.pos.doMotion;
                            character.physics.pos = new Position {
                                cell = msg.pos.offset.cell,
                                frame = new Frame(msg.pos.offset.offset, Util.quaternionFromAxisAngleLeftHanded(new Vector3(0.0f, 0.0f, 1.0f), msg.pos.heading.rotDegrees * DEG_TO_RAG)),
                            };

                            PositionPack pos = new PositionPack {
                                time = serverTime.time,
                                offset = new PositionOffset {
                                    cell = character.physics.pos.cell,
                                    offset = character.physics.pos.frame.pos,
                                },
                                doMotion = character.motion,
                                heading = new Heading(character.heading),
                                packFlags = PositionPack.PackFlag.CONTACT,
                                posStamp = (ushort)(character.physics.timestamps[0] + 1),
                            };
                            if (msg.pos.packFlags.HasFlag(CPositionPack.PackFlag.JUMP)) {
                                pos.packFlags |= PositionPack.PackFlag.JUMP;
                                pos.impulseVel = msg.pos.jumpVel;
                            }

                            // TODO: If cell has changed, might need to send PositionCellMsg instead
                            playerManager.broadcastSend(player.clientId, new PositionMsg {
                                senderIdWithStamp = character.getInstanceIdWithStamp(),
                                pos = pos,
                            });

                            character.physics.timestamps[0]++;
                        }

                        break;
                    }
                default: {
                        Log.Error($"Unhandled opcode: {genericMsg.opcode} - message not processed!");
                        handled = false;
                        break;
                    }
            }
            return handled;
        }

        private void sendCharacters(Player player) {
            List<CharacterIdentity> characterIdentities = new List<CharacterIdentity>();
            foreach (Character character in characterManager.getWithAccount(player.account.id)) {
                WorldObject playerObject = objectManager.get(character.worldObjectId);

                characterIdentities.Add(new CharacterIdentity {
                    id = playerObject.id,
                    name = playerObject.qualities.weenieDesc.name.literalValue,
                    secondsGreyedOut = 0,
                    visualDesc = playerObject.visual,
                });
            }

            List<string> characterNames = new List<string>();
            List<InstanceId> characterIds = new List<InstanceId>();
            foreach (CharacterIdentity characterIdentity in characterIdentities) {
                characterNames.Add(characterIdentity.name);
                characterIds.Add(characterIdentity.id);
            }

            packetHandler.send(player.clientId, new MinCharSetMsg {
                numAllowedCharacters = 0,
                accountName = player.account.userName,
                characterNames = characterNames,
                characterIds = characterIds,
            });

            packetHandler.send(player.clientId, new CharacterSetMsg {
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
        }

        public void save() {
            WorldSave worldSave = new WorldSave();
            characterManager.contributeToSave(worldSave);
            objectManager.contributeToSave(worldSave);
            worldDb.save(worldSave);
        }

        public void disconnectAll() {
            playerManager.disconnectAll();
        }
    }
}
