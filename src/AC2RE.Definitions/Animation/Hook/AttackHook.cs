using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class AttackHook : IPackage {

        public virtual PackageType packageType => PackageType.AttackHook;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            AREA_OF_EFFECT = 1 << 0, // 0x00000001, AttackHook::IsAreaOfEffect
            INTERSECTION = 1 << 1, // 0x00000002, AttackHook::IsIntersection
            PROJECTILE = 1 << 2, // 0x00000004, AttackHook::IsProjectile
            DISTANCE = 1 << 3, // 0x00000008, AttackHook::IsDistance
            REQUIRES_LOS = 1 << 4, // 0x00000010, AttackHook::RequiresLOS
            DETONATES_ON_MISS = 1 << 5, // 0x00000020, AttackHook::DetonatesOnMiss
            NON_DAMAGING = 1 << 6, // 0x00000040, AttackHook::IsNonDamaging
            MISSILE_THROWN = 1 << 7, // 0x00000080, AttackHook::IsMissileThrown
            MISSILE_CLONE = 1 << 8, // 0x00000100, AttackHook::ShouldMissileClone
            MISSILE_ALWAYS_FIRE_TOWARDS_TARGET = 1 << 9, // 0x00000200, AttackHook::SetMissileAlwaysFireTowardsTarget
            REQUIRES_DUAL_WIELD = 1 << 10, // 0x00000400, AttackHook::RequiresDualWield
            DETONATION_PROXIMITY = 1 << 11, // 0x00000800, AttackHook::SetDetonationProximity
            SECONDARY_IMPLEMENT = 1 << 12, // 0x00001000, AttackHook::IsSecondaryImplement
            PRIMARY_IMPLEMENT = 1 << 13, // 0x00002000, AttackHook::IsPrimaryImplement
            SHIELD_ALLOWED = 1 << 14, // 0x00004000, AttackHook::IsShieldAllowed
        }

        public float attackAreaRadius; // mAttackAreaRadius
        public float hookSkillLevel; // m_fHookSkillLevel
        public float chanceToFire; // m_chanceToFire
        public float missileTargetOffset; // m_missileTargetOffset
        public FxId weaponFxCrit; // mWeaponFXCrit
        public FxId attackerFxCrit; // mAttackerFXCrit
        public DataId missileEntityDid; // m_missileEntityDID
        public BehaviorId weaponBehaviorHit; // mWeaponBehaviorHit
        public BehaviorId targetBehaviorCrit; // mTargetBehaviorCrit
        public FxId weaponFxMiss; // mWeaponFXMiss
        public SingletonPkg<Effect> requiredEffect; // m_RequiredEffect
        public float missileSourceOffset; // m_missileSourceOffset
        public List<SingletonPkg<Effect>> userEffects; // m_UserEffects
        public uint animHookIndex; // m_animHookIndex
        public BehaviorId targetBehaviorMiss; // mTargetBehaviorMiss
        public FxId targetFxHit; // mTargetFXHit
        public VectorPkg missileSourceOffsetVector; // m_missileSourceOffsetVector
        public uint missileHoldingLocation; // m_missileHoldingLocation
        public FxId attackerFxHit; // mAttackerFXHit
        public Flag flags; // m_flags
        public FxId targetFxMiss; // mTargetFXMiss
        public MissileParameters missileParams; // m_missileParams
        public float requiredEffectWeight; // m_fRequiredEffectWeight
        public float missileInitialSpeed; // m_missileInitialSpeed
        public List<SingletonPkg<Effect>> targetEffects; // m_TargetEffects
        public BehaviorId weaponBehaviorCrit; // mWeaponBehaviorCrit
        public BehaviorId weaponBehaviorMiss; // mWeaponBehaviorMiss
        public FxId weaponFxHit; // mWeaponFXHit
        public FxId attackerFxMiss; // mAttackerFXMiss
        public BehaviorId targetBehaviorHit; // mTargetBehaviorHit
        public FxId targetFxCrit; // mTargetFXCrit

        public AttackHook(AC2Reader data) {
            attackAreaRadius = data.ReadSingle();
            hookSkillLevel = data.ReadSingle();
            chanceToFire = data.ReadSingle();
            missileTargetOffset = data.ReadSingle();
            weaponFxCrit = (FxId)data.ReadUInt32();
            attackerFxCrit = (FxId)data.ReadUInt32();
            missileEntityDid = data.ReadDataId();
            weaponBehaviorHit = (BehaviorId)data.ReadUInt32();
            targetBehaviorCrit = (BehaviorId)data.ReadUInt32();
            weaponFxMiss = (FxId)data.ReadUInt32();
            data.ReadPkg<Effect>(v => requiredEffect = v);
            missileSourceOffset = data.ReadSingle();
            data.ReadPkg<RList>(v => userEffects = v.to(SingletonPkg<Effect>.cast));
            animHookIndex = data.ReadUInt32();
            targetBehaviorMiss = (BehaviorId)data.ReadUInt32();
            targetFxHit = (FxId)data.ReadUInt32();
            data.ReadPkg<VectorPkg>(v => missileSourceOffsetVector = v);
            missileHoldingLocation = data.ReadUInt32();
            attackerFxHit = (FxId)data.ReadUInt32();
            flags = (Flag)data.ReadUInt32();
            targetFxMiss = (FxId)data.ReadUInt32();
            data.ReadPkg<MissileParameters>(v => missileParams = v);
            requiredEffectWeight = data.ReadSingle();
            missileInitialSpeed = data.ReadSingle();
            data.ReadPkg<RList>(v => targetEffects = v.to(SingletonPkg<Effect>.cast));
            weaponBehaviorCrit = (BehaviorId)data.ReadUInt32();
            weaponBehaviorMiss = (BehaviorId)data.ReadUInt32();
            weaponFxHit = (FxId)data.ReadUInt32();
            attackerFxMiss = (FxId)data.ReadUInt32();
            targetBehaviorHit = (BehaviorId)data.ReadUInt32();
            targetFxCrit = (FxId)data.ReadUInt32();
        }
    }
}
