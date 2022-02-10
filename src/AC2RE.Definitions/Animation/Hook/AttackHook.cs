using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class AttackHook : IHeapObject {

    public virtual PackageType packageType => PackageType.AttackHook;

    // WLib AttackHook
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsAreaOfEffect = 1 << 0, // IsAreaOfEffect 0x00000001
        IsIntersection = 1 << 1, // IsIntersection 0x00000002
        IsProjectile = 1 << 2, // IsProjectile 0x00000004
        IsDistance = 1 << 3, // IsDistance 0x00000008
        RequiresLOS = 1 << 4, // RequiresLOS 0x00000010
        DetonatesOnMiss = 1 << 5, // DetonatesOnMiss 0x00000020
        IsNonDamaging = 1 << 6, // IsNonDamaging 0x00000040
        IsMissileThrown = 1 << 7, // IsMissileThrown 0x00000080
        ShouldMissileClone = 1 << 8, // ShouldMissileClone 0x00000100
        ShouldMissileAlwaysFireTowardsTarget = 1 << 9, // SetMissileAlwaysFireTowardsTarget 0x00000200
        RequiresDualWield = 1 << 10, // RequiresDualWield 0x00000400
        HasDetonationProximity = 1 << 11, // SetDetonationProximity 0x00000800
        IsSecondaryImplement = 1 << 12, // IsSecondaryImplement 0x00001000
        IsPrimaryImplement = 1 << 13, // IsPrimaryImplement 0x00002000
        IsShieldAllowed = 1 << 14, // IsShieldAllowed 0x00004000
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
    public VectorHeapObject missileSourceOffsetVector; // m_missileSourceOffsetVector
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
        data.ReadHO<Effect>(v => requiredEffect = v);
        missileSourceOffset = data.ReadSingle();
        data.ReadHO<RList>(v => userEffects = v.to(SingletonPkg<Effect>.cast));
        animHookIndex = data.ReadUInt32();
        targetBehaviorMiss = (BehaviorId)data.ReadUInt32();
        targetFxHit = (FxId)data.ReadUInt32();
        data.ReadHO<VectorHeapObject>(v => missileSourceOffsetVector = v);
        missileHoldingLocation = data.ReadUInt32();
        attackerFxHit = (FxId)data.ReadUInt32();
        flags = (Flag)data.ReadUInt32();
        targetFxMiss = (FxId)data.ReadUInt32();
        data.ReadHO<MissileParameters>(v => missileParams = v);
        requiredEffectWeight = data.ReadSingle();
        missileInitialSpeed = data.ReadSingle();
        data.ReadHO<RList>(v => targetEffects = v.to(SingletonPkg<Effect>.cast));
        weaponBehaviorCrit = (BehaviorId)data.ReadUInt32();
        weaponBehaviorMiss = (BehaviorId)data.ReadUInt32();
        weaponFxHit = (FxId)data.ReadUInt32();
        attackerFxMiss = (FxId)data.ReadUInt32();
        targetBehaviorHit = (BehaviorId)data.ReadUInt32();
        targetFxCrit = (FxId)data.ReadUInt32();
    }
}
