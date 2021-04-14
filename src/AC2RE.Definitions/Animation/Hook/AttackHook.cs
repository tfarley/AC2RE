using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class AttackHook : IPackage {

        public virtual PackageType packageType => PackageType.AttackHook;

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
        public uint flags; // m_flags
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
            data.ReadSingletonPkg<Effect>(v => requiredEffect = v);
            missileSourceOffset = data.ReadSingle();
            data.ReadPkg<RList>(v => userEffects = v.to(SingletonPkg<Effect>.cast));
            animHookIndex = data.ReadUInt32();
            targetBehaviorMiss = (BehaviorId)data.ReadUInt32();
            targetFxHit = (FxId)data.ReadUInt32();
            data.ReadPkg<VectorPkg>(v => missileSourceOffsetVector = v);
            missileHoldingLocation = data.ReadUInt32();
            attackerFxHit = (FxId)data.ReadUInt32();
            flags = data.ReadUInt32();
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
