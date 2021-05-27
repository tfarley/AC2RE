using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum AttackHookFlag : uint {
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
}
