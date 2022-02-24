using AC2RE.Definitions;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Server;

internal class CombatMessageProcessor : BaseMessageProcessor {

    private int toggleCounter = 0;
    private EffectId testEffectId;

    public CombatMessageProcessor(World world) : base(world) {

    }

    public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
        switch (genericMsg.opcode) {
            case MessageOpcode.Interp__InterpSEvent: {
                    InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                    // TODO: Just for testing - when pressing the attack mode button, toggle Refining effect UI image
                    if (msg.netEvent.funcId == ServerEventFunctionId.Combat__StartAttack) {
                        if (tryGetCharacter(player, out WorldObject? character)) {
                            character.doFx(FxId.Portal_Use, 1.0f);

                            if (toggleCounter % 2 == 0) {
                                testEffectId = character.addEffect(new() {
                                    timeDemotedFromTopLevel = -1.0,
                                    timeCast = world.serverTime.time,
                                    casterId = player.characterId,
                                    timeout = 0.0f,
                                    appFloat = 0.0f,
                                    spellcraft = 1.0f,
                                    appInt = 0,
                                    pk = false,
                                    appPackage = null,
                                    timePromotedToTopLevel = -1.0,
                                    effect = new() {
                                        wstateDid = new(0x6F0011ED),
                                    },
                                    actingForWhomId = default,
                                    skillDid = default,
                                    fromItemId = player.characterId,
                                    flags = EffectRecord.Flag.IsInfiniteTimeout | EffectRecord.Flag.IsRemoveOnLogout | EffectRecord.Flag.IsTransientEffect,
                                    durabilityLevel = 0,
                                    relatedEffectId = EffectId.NULL,
                                    effectId = EffectId.NULL,
                                    categories = 1,
                                    maxDurabilityLevel = 0,
                                });
                            } else {
                                character.removeEffect(testEffectId);
                            }
                        }

                        WorldObject newTestObject = world.objectManager.create(InstanceId.IdType.Dynamic);
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

                        toggleCounter++;
                    } else if (msg.netEvent.funcId == ServerEventFunctionId.Combat__StopAttack) {
                        if (tryGetCharacter(player, out WorldObject? character)) {
                            character.setAttacking(false);
                        }
                    } else if (msg.netEvent.funcId == ServerEventFunctionId.Combat__DoAttack) {
                        DoAttackSEvt sEvent = (DoAttackSEvt)msg.netEvent;
                        if (tryGetCharacter(player, out WorldObject? character)) {
                            character.doAttack(sEvent.skillId, sEvent.targetId, sEvent.specialAttackId);
                        }
                    } else if (msg.netEvent.funcId == ServerEventFunctionId.Death__RequestResurrect) {
                        if (tryGetCharacter(player, out WorldObject? character)) {
                            character.resurrect();
                        }
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
