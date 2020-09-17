﻿using AC2E.Def;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AC2E.Server {

    internal class World {

        private static readonly Position TUTORIAL_START_POS = new Position {
            cell = new CellId(0x02, 0x98, 0x01, 0x09),
            frame = new Frame(new Vector3(59.060577f, 240.199f, -44.894524f), new Quaternion(0.70710677f, 0.0f, 0.0f, 0.70710677f)),
        };

        private static readonly Position ARWIC_START_POS = new Position {
            cell = new CellId(0x75, 0xB9, 0x00, 0x31),
            frame = new Frame(new Vector3(131.13126f, 13.535009f, 127.25996f), new Quaternion(1.0f, 0.0f, 0.0f, 0.0f)),
        };

        private readonly ServerTime serverTime;
        private readonly PacketHandler packetHandler;

        private readonly Dictionary<ClientId, Player> players = new Dictionary<ClientId, Player>();
        private readonly Dictionary<InstanceId, WorldObject> worldObjects = new Dictionary<InstanceId, WorldObject>();

        private int toggleCounter = 0;

        public World(ServerTime serverTime, PacketHandler packetHandler) {
            this.serverTime = serverTime;
            this.packetHandler = packetHandler;
        }

        public void addPlayer(ClientId clientId, Account account) {
            players[clientId] = new Player(clientId, account);
        }

        public bool playerExists(ClientId clientId) {
            return players.ContainsKey(clientId);
        }

        public void addObject(WorldObject worldObject) {
            worldObjects[worldObject.id] = worldObject;
        }

        public void removeObject(WorldObject worldObject) {
            worldObjects.Remove(worldObject.id);
        }

        public bool processMessage(ClientId clientId, INetMessage genericMsg) {
            Player player = players[clientId];

            bool handled = true;
            switch (genericMsg.opcode) {
                case MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT: {
                        CliDatInterrogationResponseMsg msg = (CliDatInterrogationResponseMsg)genericMsg;

                        List<CharacterIdentity> characterIdentities = new List<CharacterIdentity>();
                        foreach (Character character in player.account.characters) {
                            characterIdentities.Add(character.toIdentity());
                        }

                        List<string> characterNames = new List<string>();
                        List<InstanceId> characterIds = new List<InstanceId>();
                        foreach (CharacterIdentity characterIdentity in characterIdentities) {
                            characterNames.Add(characterIdentity.name);
                            characterIds.Add(characterIdentity.id);
                        }

                        packetHandler.send(player.clientId, new CliDatEndDDDMsg());

                        packetHandler.send(player.clientId, new MinCharSetMsg {
                            numAllowedCharacters = 0,
                            accountName = player.account.accountName,
                            characterNames = characterNames,
                            characterIds = characterIds,
                        });

                        packetHandler.send(player.clientId, new CharacterSetMsg {
                            characters = characterIdentities,
                            deletedCharacters = null,
                            status = 0,
                            numAllowedCharacters = 10,
                            accountName = player.account.accountName,
                            unk1 = 1,
                            hasLegions = true,
                            useTurbineChat = true,
                        });
                        break;
                    }
                case MessageOpcode.CHARACTER_CREATE_EVENT: {
                        CharacterCreateMsg msg = (CharacterCreateMsg)genericMsg;
                        // TODO: Create character
                        break;
                    }
                case MessageOpcode.CHARACTER_ENTER_GAME_EVENT: {
                        CharacterEnterGameMsg msg = (CharacterEnterGameMsg)genericMsg;

                        packetHandler.send(player.clientId, new CreatePlayerMsg {
                            id = msg.characterId,
                            regionId = 1,
                        });

                        packetHandler.send(player.clientId, new PlayerDescMsg {
                            qualities = new CBaseQualities {
                                packFlags = CBaseQualities.PackFlag.WEENIE_DESC | CBaseQualities.PackFlag.INT_HASH_TABLE | CBaseQualities.PackFlag.BOOL_HASH_TABLE | CBaseQualities.PackFlag.FLOAT_HASH_TABLE | CBaseQualities.PackFlag.TIMESTAMP_HASH_TABLE | CBaseQualities.PackFlag.DATA_ID_HASH_TABLE | CBaseQualities.PackFlag.LONG_INT_HASH_TABLE,
                                did = new DataId(0x81000530),
                                weenieDesc = new WeenieDesc {
                                    packFlags = WeenieDesc.PackFlag.NAME | WeenieDesc.PackFlag.MONARCH_ID | WeenieDesc.PackFlag.PHYSICS_TYPE_LOW_DWORD | WeenieDesc.PackFlag.PHYSICS_TYPE_HIGH_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_HIGH_DWORD,
                                    name = new StringInfo("Diabesus [M]"),
                                    monarchId = new InstanceId(0x2130000000003B2D),
                                    physicsTypeLow = 75497504,
                                    physicsTypeHigh = 0,
                                    movementEtherealLow = 1042284560,
                                    movementEtherealHigh = 0,
                                    placementEtherealLow = 65011856,
                                    placementEtherealHigh = 0,
                                },
                                intTable = new Dictionary<IntStat, int> {
                                    { IntStat.CONTAINERMAXCAPACITY, 78 },
                                    { IntStat.SPECIES, 1 },
                                    { IntStat.SEX, 4096 },
                                    { IntStat.CLASS, 2 },
                                    { IntStat.LEVEL, 7 },
                                    { IntStat.GROOVELEVEL, -1 },
                                    { IntStat.MONEY, 391 },
                                    { IntStat.HEALTH_CURRENTLEVEL, 310 },
                                    { IntStat.VIGOR_CURRENTLEVEL, 280 },
                                    { IntStat.HEALTH_CACHEDMAX, 280 },
                                    { IntStat.VIGOR_CACHEDMAX, 280 },
                                },
                                longTable = new Dictionary<LongIntStat, long> {
                                    { LongIntStat.TOTALXP, 902 },
                                    { LongIntStat.AVAILABLEXP, 722 },
                                    { LongIntStat.TOTALCRAFTXP, 80 },
                                    { LongIntStat.AVAILABLECRAFTXP, 40 },
                                },
                                boolTable = new Dictionary<BoolStat, bool> {
                                    { BoolStat.PLAYER_ISONMOUNT, false }
                                },
                                floatTable = new Dictionary<FloatStat, float> {
                                    { FloatStat.CURRENTVITAE, 100.0f },
                                    { FloatStat.HEALTH_REGENRATE, 1.0f },
                                    { FloatStat.VIGOR_REGENRATE, 1.0f },
                                    { FloatStat.SKILL_RESETTIMEDURATION, 30.0f },
                                },
                                doubleTable = new Dictionary<TimestampStat, double> {
                                    { TimestampStat.SKILL_TIMELASTRESET, 121629267.45585053 }
                                },
                                dataIdTable = new Dictionary<DataIdStat, DataId> {
                                    { DataIdStat.PHYSOBJ, new DataId(0x470000CD) }
                                },
                            },
                        });

                        packetHandler.send(player.clientId, new CreateObjectMsg {
                            id = msg.characterId,
                            visualDesc = new VisualDesc {
                                packFlags = VisualDesc.PackFlag.PARENT | VisualDesc.PackFlag.SCALE | VisualDesc.PackFlag.GLOBALMOD,
                                parentDid = new DataId(0x1F000023),
                                scale = new Vector3(0.9107999f, 0.9107999f, 0.98999995f),
                                globalAppearanceModifiers = new PartGroupDataDesc {
                                    packFlags = PartGroupDataDesc.PackFlag.KEY | PartGroupDataDesc.PackFlag.APPHASH,
                                    key = PartGroupDataDesc.PartGroupKey.ENTIRE_TREE,
                                    appearanceInfos = new Dictionary<uint, AppearanceInfo> {
                                        { 536870990, new AppearanceInfo {
                                            appearances = new Dictionary<uint, float> {
                                                { 2, 0.14f },
                                                { 16, 1.0f },
                                            }
                                        } },
                                        { 536870992, new AppearanceInfo {
                                            appearances = new Dictionary<uint, float> {
                                                { 2, 0.24f },
                                                { 16, 1.0f },
                                            }
                                        } },
                                        { 536870924, new AppearanceInfo {
                                            appearances = new Dictionary<uint, float> {
                                                { 1, 0.3f },
                                            }
                                        } },
                                        { 536870925, new AppearanceInfo {
                                            appearances = new Dictionary<uint, float> {
                                                { 3, 0.2f },
                                                { 11, 0.15f },
                                                { 12, 0.3f },
                                            }
                                        } },
                                        { 536870926, new AppearanceInfo {
                                            appearances = new Dictionary<uint, float> {
                                                { 1090519068, 0.06f },
                                            }
                                        } },
                                        { 536871161, new AppearanceInfo {
                                            appearances = new Dictionary<uint, float> {
                                                { 2, 0.04f },
                                                { 16, 1.0f },
                                            }
                                        } },
                                        { 536870934, new AppearanceInfo {
                                            appearances = new Dictionary<uint, float> {
                                                { 2, 0.2f },
                                                { 16, 1.0f },
                                            }
                                        } },
                                    }
                                },
                            },
                            physicsDesc = new PhysicsDesc {
                                packFlags = PhysicsDesc.PackFlag.SLIDERS | PhysicsDesc.PackFlag.MODE | PhysicsDesc.PackFlag.POSITION | PhysicsDesc.PackFlag.VELOCITY_SCALE,
                                sliders = new Dictionary<uint, PhysicsDesc.SliderData> {
                                    { 1073741834, new PhysicsDesc.SliderData {
                                        value = 1.0f,
                                        velocity = 0.0f,
                                    } }
                                },
                                modeId = 1073741825,
                                pos = ARWIC_START_POS,
                                velScale = 20.0f,
                                timestamps = new ushort[] { 1, 0, 0, 0 },
                                instanceStamp = 5,
                                visualOrderStamp = 8,
                            },
                            weenieDesc = new WeenieDesc {
                                packFlags = WeenieDesc.PackFlag.MY_PACKAGE_ID | WeenieDesc.PackFlag.NAME | WeenieDesc.PackFlag.MONARCH_ID | WeenieDesc.PackFlag.PHYSICS_TYPE_LOW_DWORD | WeenieDesc.PackFlag.PHYSICS_TYPE_HIGH_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.MOVEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_LOW_DWORD | WeenieDesc.PackFlag.PLACEMENT_ETHEREAL_HIGH_DWORD | WeenieDesc.PackFlag.ENTITY_DID,
                                packageId = new PackageId(895),
                                name = new StringInfo("Diabesus [M]"),
                                monarchId = new InstanceId(0x2130000000003B2D),
                                physicsTypeLow = 75497504,
                                physicsTypeHigh = 0,
                                movementEtherealLow = 1042284560,
                                movementEtherealHigh = 0,
                                placementEtherealLow = 65011856,
                                placementEtherealHigh = 0,
                                entityDid = new DataId(0x47000530),
                            },
                        });

                        packetHandler.send(player.clientId, new InterpCEventPrivateMsg {
                            netEvent = new HandleCharacterSessionStartCEvt {
                                money = 12345,
                                actRegistry = new ActRegistry {
                                    viewingProtectionEffectId = 0,
                                    actSceneTable = new ARHash<AList> {
                                        contents = new Dictionary<uint, AList> {
                                            { 0x40000005, new AList() },
                                            { 0x40000006, new AList() },
                                            { 0x40000007, new AList() },
                                            { 0x40000008, new AList() },
                                            { 0x40000009, new AList() },
                                            { 0x4000000A, new AList() },
                                        }
                                    }
                                },
                                quests = new GMQuestInfoList {
                                    contents = new List<GMQuestInfo> {
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
                                    filters = new Dictionary<uint, uint> {
                                        { 0x00800001, 0x0060017B },
                                        { 0x00000002, 0x80000000 },
                                        { 0x00000003, 0x00010000 },
                                        { 0x00000004, 0x00020000 },
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
                                    perkTypes = new AAHash {
                                        contents = new Dictionary<uint, uint> {

                                        }
                                    },
                                    typeUntrained = 0,
                                    categories = new AAHash {
                                        contents = new Dictionary<uint, uint> {

                                        }
                                    },
                                    skills = new ARHash<SkillInfo> {
                                        // Skill ids from enum mapper 0x2300000F
                                        contents = new Dictionary<uint, SkillInfo> {
                                            { (uint)SkillId.HUM_ME_RIPOSTE, new SkillInfo {
                                                lastUsedTime = -1,
                                                mask = 33,
                                                grantedTime = -1,
                                                skillOverride = 1,
                                                typeSkill = SkillId.HUM_ME_RIPOSTE,
                                            } },
                                            { (uint)SkillId.HUM_ME_UNPREDICTABLEBLOW, new SkillInfo {
                                                lastUsedTime = -1,
                                                mask = 33,
                                                grantedTime = -1,
                                                skillOverride = 1,
                                                typeSkill = SkillId.HUM_ME_UNPREDICTABLEBLOW,
                                            } },
                                            { (uint)SkillId.COM_LIFESTONERECALL, new SkillInfo {
                                                lastUsedTime = -1,
                                                mask = 33,
                                                grantedTime = -1,
                                                skillOverride = 1,
                                                typeSkill = SkillId.COM_LIFESTONERECALL,
                                            } },
                                        }
                                    },
                                },
                                effectRegistry = new EffectRegistry {
                                    qualitiesModifiedCount = null,
                                    appliedFx = new AAHash {
                                        contents = new Dictionary<uint, uint> {

                                        },
                                    },
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
                                    appliedAppearances = new AAHash {
                                        contents = new Dictionary<uint, uint> {

                                        },
                                    },
                                },
                                filledInventoryLocations = 0,
                                inventoryByLocationTable = new ARHash<InventProfile> {

                                },
                                inventoryByIdTable = new LRHash<InventProfile> {

                                },
                                containerSegments = new RList<ContainerSegmentDescriptor> {

                                },
                                containerIds = new InstanceIdList {

                                },
                                contentIds = new InstanceIdList {

                                },
                                localFactionStatus = 1,
                                serverFactionStatus = 0,
                            }
                        });
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
                                            casterId = new InstanceId(0x213000000000dd9d),
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
                                            fromItemId = new InstanceId(0x213000000000dd9d),
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

                            packetHandler.send(player.clientId, new CreateObjectMsg {
                                id = new InstanceId(0x12345 + (ulong)toggleCounter),
                                visualDesc = new VisualDesc {
                                    packFlags = VisualDesc.PackFlag.PARENT,
                                    parentDid = new DataId(0x1F000000 + (uint)toggleCounter),
                                },
                                physicsDesc = new PhysicsDesc {
                                    packFlags = PhysicsDesc.PackFlag.POSITION,
                                    pos = new Position {
                                        cell = new CellId(0x75, 0xB9, 0x00, 0x31),
                                        frame = new Frame(new Vector3(131.13126f - toggleCounter * 1.0f, 13.535009f + toggleCounter * 1.0f, 127.25996f), new Quaternion(0.70710677f, 0.0f, 0.0f, 0.70710677f)),
                                    },
                                    timestamps = new ushort[] { 1, 0, 0, 0 },
                                    instanceStamp = 5,
                                    visualOrderStamp = 8,
                                },
                                weenieDesc = new WeenieDesc {
                                    packFlags = WeenieDesc.PackFlag.MY_PACKAGE_ID | WeenieDesc.PackFlag.NAME | WeenieDesc.PackFlag.ENTITY_DID,
                                    packageId = new PackageId(895),
                                    name = new StringInfo($"TestObj 0x{toggleCounter:X}"),
                                    entityDid = new DataId(0x47000530),
                                },
                            });

                            packetHandler.send(player.clientId, new QualUpdateIntPrivateMsg {
                                type = IntStat.HEALTH_CURRENTLEVEL, // TODO: Health_CurrentLevel_IntStat
                                value = toggleCounter,
                            });

                            packetHandler.send(player.clientId, new DoFxMsg {
                                senderIdWithStamp = new InstanceIdWithStamp {
                                    id = new InstanceId(0x213000000000dd9d),
                                    instanceStamp = 5,
                                    otherStamp = 9,
                                },
                                fxId = FxId.PORTAL_USE,
                                scalar = 1.0f,
                            });

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
                        }
                        break;
                    }
                case MessageOpcode.Evt_Physics__CPosition_ID: {
                        CPositionMsg msg = (CPositionMsg)genericMsg;
                        Log.Information($"Pos: {msg.pos.offset.offset}");
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

        public void tick() {
            packetHandler.processNetBlobs(this);
        }
    }
}
