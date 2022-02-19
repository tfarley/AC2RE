using AC2RE.Definitions;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Server;

internal class CombatMessageProcessor : BaseMessageProcessor {

    private int toggleCounter = 0;

    public CombatMessageProcessor(World world) : base(world) {

    }

    public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
        switch (genericMsg.opcode) {
            case MessageOpcode.Interp__InterpSEvent: {
                    InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                    // TODO: Just for testing - when pressing the attack mode button, toggle Refining effect UI image
                    if (msg.netEvent.funcId == ServerEventFunctionId.Combat__StartAttack) {
                        if (toggleCounter % 2 == 0) {
                            SingletonPkg<Effect> refiningEffect = new() {
                                wstateDid = new(0x6F0011ED),
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
                                        flags = EffectRecord.Flag.IsInfiniteTimeout | EffectRecord.Flag.IsRemoveOnLogout | EffectRecord.Flag.IsTransientEffect,
                                        durabilityLevel = 0,
                                        relatedEffectId = EffectId.NULL,
                                        effectId = EffectId.NULL,
                                        categories = 1,
                                        maxDurabilityLevel = 0,
                                    },
                                    effectId = new(0x00000BD9),
                                }
                            });
                        } else {
                            send(player, new InterpCEventPrivateMsg {
                                netEvent = new ClientRemoveEffectCEvt {
                                    effectId = new(0x00000BD9),
                                }
                            });
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

                        if (tryGetCharacter(player, out WorldObject? character)) {
                            character.health = toggleCounter;

                            character.doFx(FxId.Portal_Use, 1.0f);
                        }

                        toggleCounter++;
                    } else if (msg.netEvent.funcId == ServerEventFunctionId.Combat__StopAttack) {
                        if (tryGetCharacter(player, out WorldObject? character)) {
                            character.setAttacking(false);
                        }
                    } else if (msg.netEvent.funcId == ServerEventFunctionId.Combat__DoAttack) {
                        DoAttackSEvt sEvent = (DoAttackSEvt)msg.netEvent;
                        if (tryGetCharacter(player, out WorldObject? character)) {
                            character.setAttacking(true);
                            Skill skill = world.contentManager.getSkill(sEvent.skillId);
                            SkillInfo skillInfo = character.skillRepo.skills[sEvent.skillId];
                            if (skill is ActiveSkill activeSkill) {
                                InstanceId weaponId = character.getEquipped(InvLoc.PrimaryHand);
                                if (weaponId == InstanceId.NULL) {
                                    weaponId = character.getEquipped(InvLoc.SecondaryHand);
                                }

                                List<StoryHookData> hooks = new();
                                for (int i = 0; i < activeSkill.hooks.Count; i++) {
                                    AttackHook hook = activeSkill.hooks[i];
                                    if (Random.Shared.NextSingle() <= hook.chanceToFire) {
                                        if (hook.flags.HasFlag(AttackHook.Flag.IsProjectile)) {
                                            hooks.Add(new() {
                                                type = StoryHookData.HookType.CreateMissiles,
                                            });
                                            hooks.Add(new() {
                                                type = StoryHookData.HookType.FireMissile,
                                                targetId = sEvent.targetId,
                                                weaponId = weaponId,
                                                animHookNumber = hook.animHookIndex,
                                                attackHookNumber = (uint)(activeSkill.hooks.Count - 1),
                                            });
                                        }
                                        if (hook is StaticAttackHook staticAttackHook) {
                                            hooks.Add(new() {
                                                type = StoryHookData.HookType.Damage,
                                                targetId = sEvent.targetId,
                                                weaponId = weaponId,
                                                attackResult = CombatResultType.Hit,
                                                animHookNumber = hook.animHookIndex,
                                                attackHookNumber = (uint)(activeSkill.hooks.Count - 1),
                                                targetHealthChange = -staticAttackHook.addMod,
                                            });
                                        } else if (hook is LinearAttackHook linearAttackHook) {
                                            int level = character.getSkillLevel(sEvent.skillId);
                                            float damage = 0.0f;
                                            if (linearAttackHook.addDmgData.Count > 1) {
                                                float damagePerLevel = (linearAttackHook.addDmgData[0].value - linearAttackHook.addDmgData[1].value) / (linearAttackHook.addDmgData[0].level - linearAttackHook.addDmgData[1].level);
                                                damage = linearAttackHook.addDmgData[1].value + damagePerLevel * (level - 1);
                                            }
                                            hooks.Add(new() {
                                                type = StoryHookData.HookType.Damage,
                                                targetId = sEvent.targetId,
                                                weaponId = weaponId,
                                                attackResult = CombatResultType.Hit,
                                                animHookNumber = hook.animHookIndex,
                                                attackHookNumber = (uint)(activeSkill.hooks.Count - 1),
                                                targetHealthChange = -(int)damage,
                                            });
                                        } else {
                                            hooks.Add(new() {
                                                targetId = sEvent.targetId,
                                                attackResult = CombatResultType.Hit,
                                            });
                                        }
                                    }
                                }
                                character.doStory(new() {
                                    attackerId = character.id,
                                    attackerBehavior = new() {
                                        behaviorId = activeSkill.behaviorOtherHit,
                                    },
                                    clientAttackContextId = ++player.attackNum,
                                    skillId = sEvent.skillId,
                                    hooks = hooks,
                                });
                                skillInfo.flags |= SkillInfo.Flag.HasTimeLastUsed;
                                skillInfo.lastUsedTime = world.serverTime.time;
                                send(player, new InterpCEventPrivateMsg {
                                    netEvent = new UpdateSkillInfoCEvt {
                                        skillInfo = skillInfo,
                                    }
                                });
                            }
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
