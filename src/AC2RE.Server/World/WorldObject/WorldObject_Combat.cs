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
                        float damage = staticAttackHook.addMod;
                        if (world.objectManager.tryGet(targetId, out WorldObject? target)) {
                            damage = target.applyDamage(damage, this);
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
                    } else if (hook is LinearAttackHook linearAttackHook) {
                        int level = getSkillLevel(skillId);
                        float damage = 0.0f;
                        if (linearAttackHook.addDmgData.Count > 1) {
                            float damagePerLevel = (linearAttackHook.addDmgData[0].value - linearAttackHook.addDmgData[1].value) / (linearAttackHook.addDmgData[0].level - linearAttackHook.addDmgData[1].level);
                            damage = linearAttackHook.addDmgData[1].value + damagePerLevel * (level - 1);
                        }
                        if (world.objectManager.tryGet(targetId, out WorldObject? target)) {
                            damage = target.applyDamage(damage, this);
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
                world.playerManager.send(player, new UpdateSkillInfoCEvt {
                    skillInfo = skillInfo,
                });
            }
        }
    }

    public void setAttacking(bool attacking) {
        if (player == null || this.attacking == attacking) {
            return;
        }

        this.attacking = attacking;
        world.playerManager.send(player, new UpdateAttackStateCEvt() {
            attacking = attacking,
        });

        syncMode();
    }

    public float applyDamage(float damage, WorldObject? attacker) {
        health = Math.Max(health - (int)damage, 0);

        doFx(FxId.BloodyDamaged, 1.0f - (health / healthMax));

        if (health <= 0 && !dead) {
            dead = true;
            selectable = false;

            doBehavior(new() {
                behaviorId = BehaviorId.death,
                modeId = ModeId.dead,
                cameraParent = true,
                cameraTarget = true,
                cameraHold = true,
            });
            if (player != null) {
                if (attacker != null) {
                    world.playerManager.send(player, new DisplayDeathMessageCEvt {
                        lastAttackerName = attacker.name,
                    });
                }
                world.playerManager.send(player, new SetMovementFrozenCEvt {
                    frozen = true,
                });
                // TODO: Add 10 second delay before death state is updated
                world.playerManager.send(player, new UpdateDeathStateCEvt {
                    dead = dead,
                });
            }
            if (attacker != null) {
                if (attacker.player != null) {
                    world.playerManager.send(attacker.player, new DisplayKillingMessageCEvt {
                        defenderName = name,
                    });
                }
                world.playerManager.sendAllVisible(id, new InterpCEventVisualMsg {
                    senderIdWithStamp = getInstanceIdWithStamp(++physics.visualOrderStamp),
                    netEvent = new AnnounceWhoKilledMeCEvt {
                        killerName = attacker.name,
                        killerId = attacker.id,
                    },
                });
            }
        }

        return damage;
    }

    public void resurrect() {
        if (!dead) {
            Logs.GENERAL.warn("Attempt to resurrect when not dead", "objId", id);
            return;
        }

        health = healthMax;
        vigor = vigorMax;
        dead = false;
        selectable = true;

        if (player != null) {
            addEffect(new() {
                timeDemotedFromTopLevel = -1.0,
                timeCast = world.serverTime.time,
                casterId = player.characterId,
                timeout = 180.0f,
                appFloat = 0.0f,
                spellcraft = 0.0f,
                appInt = 0,
                pk = false,
                appPackage = null,
                timePromotedToTopLevel = -1.0,
                effect = new() {
                    wstateDid = new(0x70000973),
                },
                actingForWhomId = default,
                skillDid = default,
                fromItemId = player.characterId,
                flags = EffectRecord.Flag.IsSpecifiedTimeout | EffectRecord.Flag.IsTransientEffect,
                durabilityLevel = 0,
                relatedEffectId = EffectId.NULL,
                categories = 1,
                maxDurabilityLevel = 0,
            });
            world.playerManager.send(player, new SetAnimationFrozenCEvt {
                frozen = true,
            });
            world.playerManager.send(player, new EnterPortalSpaceCEvt());
            setMode(ModeId.peace);
            world.playerManager.send(player, new ExitPortalSpaceCEvt());
            world.playerManager.send(player, new SetAnimationFrozenCEvt {
                frozen = false,
            });
            world.playerManager.send(player, new UpdateDeathStateCEvt {
                dead = dead,
            });
        }

        doFx(FxId.BloodyDamaged, 0.0f);
        doFx(FxId.Lifestone_Prot, 1.0f);
        doFx(FxId.Enter_World, 1.0f);

        stopBehavior(BehaviorId.death);

        doBehavior(new() {
            behaviorId = BehaviorId.Revive,
            cameraRestore = true,
        });

        if (player != null) {
            world.playerManager.send(player, new SetMovementFrozenCEvt {
                frozen = false,
            });
            stopFx(FxId.Enter_World);
        }
    }
}
