﻿using AC2E.Def;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace AC2E.Server {

    internal class AC2Server {

        private static readonly IPEndPoint ANY_ENDPOINT = new IPEndPoint(IPAddress.Any, 0);
        private static readonly int MAX_CONNECTIONS = 300;

        public NetInterface netInterface1;
        private NetInterface netInterface2;
        public bool active;

        private readonly byte[] receiveBuffer = new byte[NetPacket.MAX_SIZE];

        private ushort clientCounter = 1;
        private readonly Dictionary<ushort, ClientConnection> clients = new Dictionary<ushort, ClientConnection>();

        private float serverTime => (DateTime.Now.Ticks - Process.GetCurrentProcess().StartTime.Ticks) / TimeSpan.TicksPerSecond;

        private readonly List<Language> SUPPORTED_LANGUAGES = new List<Language> {
            Language.ENGLISH,
        };

        private int toggleCounter = 0;

        ~AC2Server() {
            // Always close in order to release system resources
            // (i.e. those that GC is not aware of, such as a socket)
            if (netInterface1 != null || netInterface2 != null) {
                Log.Warning($"Didn't disconnect AC2Server with interfaces {netInterface1}, {netInterface2} before destruction!");
                disconnect();
            }
        }

        public void start(int port = 0) {
            if (netInterface1 != null || netInterface2 != null) {
                disconnect();
            }

            netInterface1 = port != -1 ? new NetInterface(port) : null;
            netInterface2 = port != -1 ? new NetInterface(netInterface1.port + 1) : null;

            active = true;

            Log.Debug($"Initialized AC2Server.");
        }

        public void disconnect() {
            // TODO: Disconnect and clear all connections

            if (netInterface1 != null) {
                netInterface1.close();
                netInterface1 = null;
            }
            if (netInterface2 != null) {
                netInterface2.close();
                netInterface2 = null;
            }
            active = false;
        }

        public void processReceive() {
            processReceive(netInterface1);
            processReceive(netInterface2);

            foreach (ClientConnection client in clients.Values) {
                client.flushSend(netInterface1, serverTime);
            }
        }

        private void processReceive(NetInterface netInterface) {
            while (netInterface != null && netInterface.available) {
                EndPoint baseReceiveEndpoint = ANY_ENDPOINT;
                Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
                int receivedBytes = 0;
                try {
                    receivedBytes = netInterface.receiveFrom(receiveBuffer, ref baseReceiveEndpoint);
                } catch (Exception e) {
                    Log.Error(e, "Bad receive.");
                    // TODO: This should probably also disconnect the client, but getting the correct connection may be difficult
                }
                if (receivedBytes <= 0) {
                    continue;
                }

                try {
                    IPEndPoint receiveEndpoint = (IPEndPoint)baseReceiveEndpoint;

                    Log.Debug($"Rcv[{receivedBytes}] from {receiveEndpoint} - {BitConverter.ToString(receiveBuffer, 0, receivedBytes)}.");

                    processData(receiveBuffer, receivedBytes, receiveEndpoint);
                } catch (Exception e) {
                    Log.Error(e, "Exception when reading packet, discarded.");
                }
            }
        }

        internal void processData(byte[] rawData, int dataLen, IPEndPoint receiveEndpoint) {
            using (AC2Reader data = new AC2Reader(new MemoryStream(rawData, 0, dataLen))) {

                try {
                    NetPacket packet = new NetPacket(data);

                    Log.Debug($"RCVD: {packet}");

                    if (packet.logonHeader != null) {
                        Log.Debug($"Logon request: seq {packet.logonHeader.netAuth.connectionSeq} acct {packet.logonHeader.netAuth.accountName}");
                        ClientConnection client = addClient(receiveEndpoint, packet.logonHeader.netAuth.accountName);
                        if (client != null) {
                            sendConnect(client);
                        } else {
                            Log.Warning("Client tried to connect, but the number of active connections is already at the limit.");
                        }
                    } else if (clients.TryGetValue(packet.recipientId, out ClientConnection client)) {
                        if (packet.flags.HasFlag(NetPacket.Flag.LOGOFF)) {
                            // TODO: Remove client
                            Log.Information($"Client diconnected, id {packet.recipientId}.");
                            return;
                        }

                        // TODO: Need to handle client acking the re-sent (nacked) packets
                        if (packet.flags.HasFlag(NetPacket.Flag.PAK)) {
                            client.ackPacket(packet.ackHeader);
                        }

                        if (packet.flags.HasFlag(NetPacket.Flag.NAK)) {
                            foreach (uint seq in packet.nacksHeader) {
                                client.nackPacket(seq);
                            }
                        }

                        if (client.connected && packet.seq <= client.highestReceivedPacketSeq) {
                            if (!packet.flags.HasFlag(NetPacket.Flag.PAK) && !packet.flags.HasFlag(NetPacket.Flag.NAK)) {
                                Log.Warning($"Got dupe packet with seq {packet.seq}, expecting {client.highestReceivedPacketSeq}.");
                            }
                            return;
                        }

                        for (uint i = client.highestReceivedPacketSeq + 1; i < packet.seq; i++) {
                            client.nackedSeqs.Add(i);
                            Log.Warning($"Nacked seq {i}, client id {packet.recipientId}.");
                        }
                        client.highestReceivedPacketSeq = packet.seq;

                        if (packet.connectAckHeader != 0) {
                            if (packet.connectAckHeader == client.connectionAckCookie) {
                                Log.Debug($"Got good connect ack cookie from client id: {packet.recipientId}.");
                                client.connect(serverTime);
                                client.enqueueMessage(new WorldNameMsg {
                                    worldName = new StringInfo { literalValue = "MyWorld" },
                                });
                                client.enqueueMessage(new CliDatInterrogationMsg {
                                    regionId = (RegionID)1,
                                    nameRuleLanguage = Language.ENGLISH,
                                    supportedLanguages = SUPPORTED_LANGUAGES,
                                });
                            } else {
                                Log.Warning($"Got bad connect ack cookie from client id: {packet.recipientId} - {packet.connectAckHeader} sent, {client.connectionAckCookie} expected.");
                            }
                        }

                        if (packet.flags.HasFlag(NetPacket.Flag.ECHO_REQUEST)) {
                            client.echoRequestedLocalTime = packet.echoRequestHeader.localTime;
                            client.echoRequestedServerTime = serverTime;
                        }

                        if (packet.frags.Count > 0) {
                            foreach (NetBlobFrag frag in packet.frags) {
                                if (frag.fragCount == 1) {
                                    processNetBlob(client, frag);
                                } else {
                                    Log.Error($"Got fragmented packet from client id: {packet.recipientId} - reassembly not implemented yet!");
                                }
                            }
                        }
                    } else {
                        Log.Warning($"Got packet from unknown client id: {packet.recipientId}.");
                    }
                } catch (Exception e) {
                    Log.Error(e, "Error proccessing data.");
                }
            }
        }

        private void processNetBlob(ClientConnection client, NetBlobFrag blob) {
            using (AC2Reader data = new AC2Reader(new MemoryStream(blob.payload))) {

                MessageOpcode opcode = (MessageOpcode)data.ReadUInt32();
                INetMessage genericMsg = INetMessage.read(opcode, data, true);

                Log.Debug($"Got msg: {genericMsg}");

                bool handled = true;
                switch (opcode) {
                    case MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT: {
                            CliDatInterrogationResponseMsg msg = (CliDatInterrogationResponseMsg)genericMsg;

                            List<CharacterIdentity> characters = new List<CharacterIdentity> {
                                new CharacterIdentity {
                                    id = new InstanceId(0x213000000000dd9d),
                                    name = "TestChar",
                                    secondsGreyedOut = 0,
                                    visualDesc = new VisualDesc {
                                        packFlags = VisualDesc.PackFlag.PARENT,
                                        parentDid = new DataId(0x1F001110),
                                    },
                                },
                            };

                            List<string> characterNames = new List<string>();
                            List<InstanceId> characterIds = new List<InstanceId>();
                            foreach (CharacterIdentity character in characters) {
                                characterNames.Add(character.name);
                                characterIds.Add(character.id);
                            }

                            client.enqueueMessage(new CliDatEndDDDMsg());

                            client.enqueueMessage(new MinCharSetMsg {
                                numAllowedCharacters = 0,
                                accountName = client.accountName,
                                characterNames = characterNames,
                                characterIds = characterIds,
                            });

                            client.enqueueMessage(new CharacterSetMsg {
                                characters = characters,
                                deletedCharacters = null,
                                status = 0,
                                numAllowedCharacters = 10,
                                accountName = client.accountName,
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

                            client.enqueueMessage(new CreatePlayerMsg {
                                id = msg.characterId,
                                regionId = 1,
                            });

                            client.enqueueMessage(new PlayerDescMsg {
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
                                    intTable = new Dictionary<uint, int> {
                                        { 280, 78 },
                                        { 290, 1 },
                                        { 291, 4096 },
                                        { 292, 2 },
                                        { 302, 7 },
                                        { 950, -1 },
                                        { 3100, 391 },
                                        { 258, 310 },
                                        { 259, 280 },
                                        { 263, 280 },
                                        { 264, 280 },
                                    },
                                    longTable = new Dictionary<uint, long> {
                                        { 300, 902 },
                                        { 301, 722 },
                                        { 1000, 80 },
                                        { 1001, 40 },
                                    },
                                    boolTable = new Dictionary<uint, bool> {
                                        { 2002, false }
                                    },
                                    floatTable = new Dictionary<uint, float> {
                                        { 286, 100.0f },
                                        { 262, 1.0f },
                                        { 263, 1.0f },
                                        { 3000, 30.0f },
                                    },
                                    doubleTable = new Dictionary<uint, double> {
                                        { 303, 121629267.45585053 }
                                    },
                                    dataIdTable = new Dictionary<uint, DataId> {
                                        { 1, new DataId(0x470000CD) }
                                    },
                                },
                            });

                            client.enqueueMessage(new CreateObjectMsg {
                                id = msg.characterId,
                                visualDesc = new VisualDesc {
                                    packFlags = VisualDesc.PackFlag.PARENT | VisualDesc.PackFlag.SCALE | VisualDesc.PackFlag.GLOBALMOD,
                                    parentDid = new DataId(0x1F000023),
                                    scale = new Vector(0.9107999f, 0.9107999f, 0.98999995f),
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
                                    pos = new Position {
                                        cell = new CellId(0x8D, 0xB5, 0x00, 0x3E),
                                        frame = new Frame(new Vector(158.13483f, 117.91791f, 129.50496f), new Quaternion(0.23793525f, 0.0f, 0.0f, 0.971281f)),
                                    },
                                    velScale = 1.08f,
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

                            client.enqueueMessage(new InterpCEventPrivateMsg {
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
                                            contents = new Dictionary<uint, SkillInfo> {

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
                            client.enqueueMessage(new CliDatErrorMsg {
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
                                    SingletonPkg refiningEffect = new SingletonPkg {
                                        did = new DataId(0x6F0011ED),
                                    };
                                    client.enqueueMessage(new InterpCEventPrivateMsg {
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
                                    client.enqueueMessage(new InterpCEventPrivateMsg {
                                        netEvent = new ClientRemoveEffectCEvt {
                                            effectId = 0x00000BD9,
                                        }
                                    });
                                }

                                client.enqueueMessage(new CreateObjectMsg {
                                    id = new InstanceId(0x12345 + (ulong)toggleCounter),
                                    visualDesc = new VisualDesc {
                                        packFlags = VisualDesc.PackFlag.PARENT,
                                        parentDid = new DataId(0x1F000000 + (uint)toggleCounter),
                                    },
                                    physicsDesc = new PhysicsDesc {
                                        packFlags = PhysicsDesc.PackFlag.POSITION,
                                        pos = new Position {
                                            cell = new CellId(0x8D, 0xB5, 0x00, 0x3E),
                                            frame = new Frame(new Vector(158.13483f + toggleCounter * 1.0f, 117.91791f - toggleCounter * 1.0f, 129.50496f), new Quaternion(0.23793525f, 0.0f, 0.0f, 0.971281f)),
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

                                toggleCounter++;
                            }
                            break;
                        }
                    default: {
                            Log.Error($"Unhandled opcode: {opcode} - message not processed! Header: {blob}");
                            handled = false;
                            break;
                        }
                }

                if (handled && data.BaseStream.Position < data.BaseStream.Length) {
                    Log.Warning($"NetBlob was not fully read ({data.BaseStream.Position} / {data.BaseStream.Length}).");
                }
            }
        }

        private ClientConnection addClient(IPEndPoint clientEndpoint, string accountName) {
            if (clients.Count > MAX_CONNECTIONS) {
                return null;
            }

            ClientConnection client = new ClientConnection(clientCounter, clientEndpoint, accountName);
            clients[clientCounter] = client;
            clientCounter++;
            return client;
        }

        private void sendConnect(ClientConnection client) {
            client.sendPacket(netInterface1, serverTime, new NetPacket {
                connectHeader = new ConnectHeader {
                    connectionAckCookie = client.connectionAckCookie,
                    netId = client.id,
                    outgoingSeed = client.outgoingSeed,
                    incomingSeed = client.incomingSeed,
                },
            });
        }
    }
}
