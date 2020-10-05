using System.Collections.Generic;

namespace AC2E.Def {

    public class AttackHook : IPackage {

        public virtual PackageType packageType => PackageType.AttackHook;

        public float attackAreaRadius; // mAttackAreaRadius
        public float hookSkillLevel; // m_fHookSkillLevel
        public float chanceToFire; // m_chanceToFire
        public float missileTargetOffset; // m_missileTargetOffset
        public uint weaponFxCrit; // mWeaponFXCrit
        public uint attackerFxCrit; // mAttackerFXCrit
        public DataId missileEntityDid; // m_missileEntityDID
        public uint weaponBehaviorHit; // mWeaponBehaviorHit
        public uint targetBehaviorCrit; // mTargetBehaviorCrit
        public uint weaponFxMiss; // mWeaponFXMiss
        public SingletonPkg<Effect> requiredEffect; // m_RequiredEffect
        public float missileSourceOffset; // m_missileSourceOffset
        public List<SingletonPkg<IPackage>> userEffects; // m_UserEffects
        public uint animHookIndex; // m_animHookIndex
        public uint targetBehaviorMiss; // mTargetBehaviorMiss
        public uint targetFxHit; // mTargetFXHit
        public VectorPkg missileSourceOffsetVector; // m_missileSourceOffsetVector
        public uint missileHoldingLocation; // m_missileHoldingLocation
        public uint attackerFxHit; // mAttackerFXHit
        public uint flags; // m_flags
        public uint targetFxMiss; // mTargetFXMiss
        public MissileParameters missileParams; // m_missileParams
        public float requiredEffectWeight; // m_fRequiredEffectWeight
        public float missileInitialSpeed; // m_missileInitialSpeed
        public List<SingletonPkg<IPackage>> targetEffects; // m_TargetEffects
        public uint weaponBehaviorCrit; // mWeaponBehaviorCrit
        public uint weaponBehaviorMiss; // mWeaponBehaviorMiss
        public uint weaponFxHit; // mWeaponFXHit
        public uint attackerFxMiss; // mAttackerFXMiss
        public uint targetBehaviorHit; // mTargetBehaviorHit
        public uint targetFxCrit; // mTargetFXCrit

        public AttackHook(AC2Reader data) {
            attackAreaRadius = data.ReadSingle();
            hookSkillLevel = data.ReadSingle();
            chanceToFire = data.ReadSingle();
            missileTargetOffset = data.ReadSingle();
            weaponFxCrit = data.ReadUInt32();
            attackerFxCrit = data.ReadUInt32();
            missileEntityDid = data.ReadDataId();
            weaponBehaviorHit = data.ReadUInt32();
            targetBehaviorCrit = data.ReadUInt32();
            weaponFxMiss = data.ReadUInt32();
            data.ReadSingletonPkg<Effect>(v => requiredEffect = v);
            missileSourceOffset = data.ReadSingle();
            data.ReadPkg<RList>(v => userEffects = v.to<SingletonPkg<IPackage>>());
            animHookIndex = data.ReadUInt32();
            targetBehaviorMiss = data.ReadUInt32();
            targetFxHit = data.ReadUInt32();
            data.ReadPkg<VectorPkg>(v => missileSourceOffsetVector = v);
            missileHoldingLocation = data.ReadUInt32();
            attackerFxHit = data.ReadUInt32();
            flags = data.ReadUInt32();
            targetFxMiss = data.ReadUInt32();
            data.ReadPkg<MissileParameters>(v => missileParams = v);
            requiredEffectWeight = data.ReadSingle();
            missileInitialSpeed = data.ReadSingle();
            data.ReadPkg<RList>(v => targetEffects = v.to<SingletonPkg<IPackage>>());
            weaponBehaviorCrit = data.ReadUInt32();
            weaponBehaviorMiss = data.ReadUInt32();
            weaponFxHit = data.ReadUInt32();
            attackerFxMiss = data.ReadUInt32();
            targetBehaviorHit = data.ReadUInt32();
            targetFxCrit = data.ReadUInt32();
        }
    }
}
