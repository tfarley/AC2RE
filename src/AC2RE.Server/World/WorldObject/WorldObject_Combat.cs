using AC2RE.Definitions;
using System;
using System.Collections.Generic;

namespace AC2RE.Server;

internal partial class WorldObject {

    public bool attacking { get; private set; }

    public void initCombat() {

    }

    public void doAttack(SkillId skillId, InstanceId targetId, uint specialAttackId) {
        setAttacking(true);

        Skill skill = world.contentManager.getSkill(skillId);
        SkillInfo skillInfo = skillRepo.skills[skillId];
        if (skill is ActiveSkill activeSkill) {
            InstanceId weaponId = getEquipped(InvLoc.PrimaryHand);
            if (weaponId == InstanceId.NULL) {
                weaponId = getEquipped(InvLoc.SecondaryHand);
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
                            targetId = targetId,
                            weaponId = weaponId,
                            animHookNumber = hook.animHookIndex,
                            attackHookNumber = (uint)(activeSkill.hooks.Count - 1),
                        });
                    }
                    if (hook is StaticAttackHook staticAttackHook) {
                        hooks.Add(new() {
                            type = StoryHookData.HookType.Damage,
                            targetId = targetId,
                            weaponId = weaponId,
                            attackResult = CombatResultType.Hit,
                            animHookNumber = hook.animHookIndex,
                            attackHookNumber = (uint)(activeSkill.hooks.Count - 1),
                            targetHealthChange = -staticAttackHook.addMod,
                        });
                    } else if (hook is LinearAttackHook linearAttackHook) {
                        int level = getSkillLevel(skillId);
                        float damage = 0.0f;
                        if (linearAttackHook.addDmgData.Count > 1) {
                            float damagePerLevel = (linearAttackHook.addDmgData[0].value - linearAttackHook.addDmgData[1].value) / (linearAttackHook.addDmgData[0].level - linearAttackHook.addDmgData[1].level);
                            damage = linearAttackHook.addDmgData[1].value + damagePerLevel * (level - 1);
                        }
                        hooks.Add(new() {
                            type = StoryHookData.HookType.Damage,
                            targetId = targetId,
                            weaponId = weaponId,
                            attackResult = CombatResultType.Hit,
                            animHookNumber = hook.animHookIndex,
                            attackHookNumber = (uint)(activeSkill.hooks.Count - 1),
                            targetHealthChange = -(int)damage,
                        });
                    } else {
                        hooks.Add(new() {
                            targetId = targetId,
                            attackResult = CombatResultType.Hit,
                        });
                    }
                }
            }

            doStory(new() {
                attackerId = id,
                attackerBehavior = new() {
                    behaviorId = activeSkill.behaviorOtherHit,
                },
                clientAttackContextId = player != null ? ++player.attackNum : 0,
                skillId = skillId,
                hooks = hooks,
            });

            skillInfo.flags |= SkillInfo.Flag.HasTimeLastUsed;
            skillInfo.lastUsedTime = world.serverTime.time;
            if (player != null) {
                world.playerManager.send(player, new InterpCEventPrivateMsg {
                    netEvent = new UpdateSkillInfoCEvt {
                        skillInfo = skillInfo,
                    }
                });
            }
        }
    }

    public void setAttacking(bool attacking) {
        if (player == null || this.attacking == attacking) {
            return;
        }

        this.attacking = attacking;
        world.playerManager.send(player, new InterpCEventPrivateMsg {
            netEvent = new UpdateAttackStateCEvt() {
                attacking = attacking,
            }
        }, true);

        syncMode();
    }
}
