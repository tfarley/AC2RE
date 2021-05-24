using AC2RE.Definitions;
using System.Numerics;

namespace AC2RE.Server {

    internal class CombatMessageProcessor : BaseMessageProcessor {

        private int toggleCounter = 0;

        public CombatMessageProcessor(World world) : base(world) {

        }

        public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
            switch (genericMsg.opcode) {
                case MessageOpcode.Evt_Interp__InterpSEvent_ID: {
                        InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                        // TODO: Just for testing - when pressing the attack mode button, toggle Refining effect UI image
                        if (msg.netEvent.funcId == ServerEventFunctionId.Combat__StartAttack) {
                            if (toggleCounter % 2 == 0) {
                                SingletonPkg<Effect> refiningEffect = new() {
                                    did = new(0x6F0011ED),
                                };
                                send(player, new InterpCEventPrivateMsg {
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
                                            relatedEffectId = EffectId.NULL,
                                            effectId = EffectId.NULL,
                                            categories = 1,
                                            maxDurabilityLevel = 0,
                                        },
                                        effectId = new EffectId(0x00000BD9),
                                    }
                                });
                            } else {
                                send(player, new InterpCEventPrivateMsg {
                                    netEvent = new ClientRemoveEffectCEvt {
                                        effectId = new EffectId(0x00000BD9),
                                    }
                                });
                            }

                            WorldObject newTestObject = world.objectManager.create();
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

                            if (world.objectManager.tryGet(player.characterId, out WorldObject? character) && character.inWorld) {
                                character.health = toggleCounter;

                                character.doFx(FxId.PORTAL_USE, 1.0f);
                            }

                            toggleCounter++;
                        } else {
                            return false;
                        }
                        break;
                    }
                default:
                    return false;
            }
            return true;
        }
    }
}
