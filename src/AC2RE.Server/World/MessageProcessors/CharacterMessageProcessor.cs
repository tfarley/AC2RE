using AC2RE.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AC2RE.Server {

    internal class CharacterMessageProcessor : BaseMessageProcessor {

        public CharacterMessageProcessor(World world) : base(world) {

        }

        public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
            switch (genericMsg.opcode) {
                case MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT: {
                        CliDatInterrogationResponseMsg msg = (CliDatInterrogationResponseMsg)genericMsg;

                        send(player, new CliDatEndDDDMsg());
                        sendCharacters(player);

                        break;
                    }
                case MessageOpcode.CHARACTER_CREATE_EVENT: {
                        CharacterCreateMsg msg = (CharacterCreateMsg)genericMsg;

                        WorldObject characterObject = CharacterGen.createCharacterObject(world, World.ARWIC_START_POS, msg.characterName, msg.species, msg.sex, msg.physiqueTypeValues);
                        characterObject.player = player;

                        Character character = world.characterManager.createWithAccountAndObject(player.account.id, characterObject.id);

                        send(player, new CharGenVerificationMsg {
                            response = CharGenResponse.Ok,
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
                case MessageOpcode.Login__CharacterDeletion: {
                        CharacterDeletionSMsg msg = (CharacterDeletionSMsg)genericMsg;

                        if (world.objectManager.tryGet(msg.characterId, out WorldObject? character)) {
                            if (world.characterManager.tryGetWithAccountAndObject(player.account.id, character.id, out Character? characterInfo)) {
                                characterInfo.deleted = true;
                                character.delete();

                                send(player, new CharacterDeletionCMsg {
                                    characterId = character.id,
                                });
                                sendCharacters(player);
                            }
                        }

                        break;
                    }
                case MessageOpcode.CHARACTER_ENTER_GAME_EVENT: {
                        CharacterEnterGameMsg msg = (CharacterEnterGameMsg)genericMsg;

                        if (!world.characterManager.existsWithAccountAndWorldObject(player.account.id, msg.characterId)) {
                            throw new ArgumentException($"Account {player.account.id} attempted to log in with unowned character object {msg.characterId}.");
                        }

                        player.characterId = msg.characterId;

                        if (!world.objectManager.tryGet(msg.characterId, out WorldObject? character)) {
                            throw new ArgumentException($"Account {player.account.id} attempted to log in with non-existent character object {msg.characterId}.");
                        }

                        send(player, new CreatePlayerMsg {
                            id = msg.characterId,
                            regionId = 1,
                        });

                        send(player, new PlayerDescMsg {
                            qualities = character.qualities,
                        });

                        InvLoc filledInventoryLocations = InvLoc.None;
                        Dictionary<InvLoc, InventProfile> inventoryByLocationTable = new();
                        Dictionary<InstanceId, InventProfile> inventoryByIdTable = new();

                        foreach ((InvLoc equipLoc, InstanceId itemId) in character.invLocToEquippedItemIdEnumerable) {
                            if (world.objectManager.tryGet(itemId, out WorldObject? item)) {
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
                                    foreach ((DataId partDid, Dictionary<AppearanceKey, float> appearanceInfos) in item.globalAppearanceModifiers.appearanceInfos) {
                                        profile.visualDescInfo.appInfoHash[partDid] = appearanceInfos;
                                    }
                                }
                                filledInventoryLocations |= equipLoc;
                                inventoryByLocationTable[equipLoc] = profile;
                                inventoryByIdTable[item.id] = profile;
                            }
                        }

                        send(player, new InterpCEventPrivateMsg {
                            netEvent = new HandleCharacterSessionStartCEvt {
                                money = 12345,
                                actRegistry = new() {
                                    viewingProtectionEffectId = EffectId.NULL,
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
                                        questId = QuestId.FindExplorerArwic,
                                        questName = new(new(0x250017EB), 2824895724),
                                        questDescription = new(new(0x250017EB), 1816499044),
                                        iconDid = new(0x4100034B),
                                        challengeLevel = -999,
                                        questStatus = QuestStatus.Underway,
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
                                    shortcutArray = Enumerable.Repeat(new ShortcutInfo { type = ShortcutType.Undef }, 100).ToList(),
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
                                        { 1, TextType.All },
                                        { 2, TextType.GeneralChannel },
                                        { 3, TextType.Fellowship },
                                        { 4, TextType.Allegiance },
                                    },
                                    bitfield = (GameplayOptionsProfile.Flag)0x80024FF5,
                                    version = GameplayOptionsProfile.Version.LATEST_VERSION,
                                    chatFontColors = new() {
                                        { TextType.Error, 0 },
                                        { TextType.Combat, 1 },
                                        { TextType.Admin, 2 },
                                        { TextType.Standard, 3 },
                                        { TextType.Say, 4 },
                                        { TextType.Tell, 5 },
                                        { TextType.Emote, 6 },
                                        { TextType.Log, 4 },
                                        { TextType.Broadcast, 9 },
                                        { TextType.Fellowship, 7 },
                                        { TextType.Allegiance, 8 },
                                        { TextType.ChatEntry, 4 },
                                        { TextType.GeneralChannel, 4 },
                                        { TextType.TradeChannel, 4 },
                                        { TextType.RegionChannel, 4 },
                                        { TextType.FactionChannel, 4 },
                                        { TextType.Devoted, 4 },
                                        { TextType.PKChannel, 4 },
                                    },
                                    chatFontSizes = new() {
                                        { TextType.Error, 0 },
                                        { TextType.Combat, 0 },
                                        { TextType.Admin, 0 },
                                        { TextType.Standard, 0 },
                                        { TextType.Say, 0 },
                                        { TextType.Tell, 0 },
                                        { TextType.Emote, 0 },
                                        { TextType.Log, 0 },
                                        { TextType.Broadcast, 0 },
                                        { TextType.Fellowship, 0 },
                                        { TextType.Allegiance, 0 },
                                        { TextType.ChatEntry, 0 },
                                        { TextType.GeneralChannel, 0 },
                                        { TextType.TradeChannel, 0 },
                                        { TextType.RegionChannel, 0 },
                                        { TextType.FactionChannel, 0 },
                                        { TextType.Devoted, 0 },
                                        { TextType.PKChannel, 0 },
                                    },
                                    windowToChannel = new() {
                                        { 1, TextType.Say },
                                        { 2, TextType.GeneralChannel },
                                        { 3, TextType.Fellowship },
                                        { 4, TextType.Allegiance },
                                    },
                                    chatPopupFlags = new() {
                                        { TextType.Broadcast, true },
                                        { TextType.Fellowship, true },
                                        { TextType.Allegiance, true },
                                    },
                                    windowOpacities = new() {
                                        { 0xA05C6B95, 0.65f },
                                        { 0xA0446B95, 0.65f },
                                        { 0xA04C6B95, 0.65f },
                                        { 0xA0746B95, 0.65f },
                                    },
                                },
                                skills = character.skillRepo,
                                effectRegistry = new() {
                                    qualitiesModifiedCount = null,
                                    appliedFx = new(),
                                    baseEffectRegistry = null,
                                    effectIdCounter = 3,
                                    effectInfo = null,
                                    lastPulseTime = -1.0,
                                    equipperEffectIds = null,
                                    acquirerEffectIds = null,
                                    flags = (EffectRegistry.Flag)0x000C0001,
                                    trackedEffects = null,
                                    topEffects = null,
                                    effectCategorizationTable = null,
                                    appliedAppearances = new(),
                                },
                                filledInventoryLocations = filledInventoryLocations,
                                inventoryByLocationTable = inventoryByLocationTable,
                                inventoryByIdTable = inventoryByIdTable,
                                containerSegments = new() {
                                    new() {
                                        segmentMaxSize = 42,
                                        segmentSize = (uint)character.contentsItemIdsEnumerable.Count(),
                                    },
                                    new() {
                                        segmentMaxSize = 12,
                                        segmentSize = 0,
                                    },
                                    new() {
                                        segmentMaxSize = 12,
                                        segmentSize = 0,
                                    },
                                    new() {
                                        segmentMaxSize = 12,
                                        segmentSize = 0,
                                    },
                                },
                                containerIds = new() {

                                },
                                contentIds = new(character.contentsItemIdsEnumerable),
                                localFactionStatus = FactionStatus.Peace,
                                serverFactionStatus = FactionStatus.Undef,
                            }
                        });

                        foreach (InstanceId itemId in character.containedItemIdsEnumerable) {
                            if (world.objectManager.tryGet(itemId, out WorldObject? item)) {
                                item.enterWorld();
                            }
                        }

                        character.doMode(ModeId.peace);
                        character.setVelScale(2.0f);

                        character.enterWorld();

                        world.landblockManager.syncPlayerVisibility(player);
                        break;
                    }
                case MessageOpcode.Login__CharExitGame: {
                        CharacterExitGameSMsg msg = (CharacterExitGameSMsg)genericMsg;

                        if (player.characterId == msg.characterId) {
                            player.visibleObjectIds.Clear();
                            player.characterId = InstanceId.NULL;

                            send(player, new CharacterExitGameCMsg());
                            sendCharacters(player);
                        }

                        break;
                    }
                case MessageOpcode.Physics__CLookAtDir: {
                        CLookAtDirMsg msg = (CLookAtDirMsg)genericMsg;

                        if (tryGetCharacter(player, out WorldObject? character)) {
                            character.setLookAt(msg.x, msg.z);
                        }

                        break;
                    }
                case MessageOpcode.Physics__CPosition: {
                        CPositionMsg msg = (CPositionMsg)genericMsg;

                        if (tryGetCharacter(player, out WorldObject? character)) {
                            character.offset = msg.posPack.offset;
                            character.heading = msg.posPack.heading.rotDegrees;
                            character.motion = msg.posPack.doMotion;
                            character.jumped = msg.posPack.packFlags.HasFlag(CPositionPack.PackFlag.JUMP);
                            character.impulseVel = msg.posPack.jumpVel;
                        }

                        break;
                    }
                default:
                    return false;
            }
            return true;
        }

        private void sendCharacters(Player player) {
            List<CharacterIdentity> characterIdentities = new();
            foreach (Character characterInfo in world.characterManager.getWithAccount(player.account.id)) {
                if (world.objectManager.tryGet(characterInfo.objectId, out WorldObject? character)) {
                    character.player = player;
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

            send(player, new MinCharSetMsg {
                numAllowedCharacters = 0,
                accountName = player.account.userName,
                characterNames = characterNames,
                characterIds = characterIds,
            });

            send(player, new CharacterSetMsg {
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
    }
}
