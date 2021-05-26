using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum ActiveSkillFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        USE_WHILE_MOVING = 1 << 0, // 0x00000001, ActiveSkill::GetUseWhileMoving
        IGNORE_WEAPON_TIME = 1 << 1, // 0x00000002, ActiveSkill::GetIgnoreWeaponTime
        IGNORE_ARMOR_DELAY = 1 << 2, // 0x00000004, ActiveSkill::GetIgnoreArmorDelay
        IGNORE_WEAPON_OFFENSE = 1 << 3, // 0x00000008, ActiveSkill::GetIgnoreWeaponOffense
        IGNORE_WEAPON_DEFENSE = 1 << 4, // 0x00000010, ActiveSkill::GetIgnoreWeaponDefense
        IGNORE_WEAPON_VIGOR = 1 << 5, // 0x00000020, ActiveSkill::GetIgnoreWeaponVigor
        IGNORE_WEAPON_DAMAGE = 1 << 6, // 0x00000040, ActiveSkill::GetIgnoreWeaponDamage
        IGNORE_SHIELD_VIGOR = 1 << 7, // 0x00000080, ActiveSkill::GetIgnoreShieldVigor
        IGNORE_SHIELD_DELAY = 1 << 8, // 0x00000100, ActiveSkill::GetIgnoreShieldDelay
        NON_DAMAGING = 1 << 9, // 0x00000200, ActiveSkill::IsNonDamaging
        HARMFUL = 1 << 10, // 0x00000400, ActiveSkill::IsHarmful
        REQUIRES_MOVE_TO = 1 << 11, // 0x00000800, ActiveSkill::RequiresMoveTo
        REQUIRES_TURN_TO = 1 << 12, // 0x00001000, ActiveSkill::RequiresTurnTo
        SELF_TARGETED = 1 << 13, // 0x00002000, ActiveSkill::IsSelfTargeted
        SUCCEED_ON_ANIMATION_FAILURE = 1 << 14, // 0x00004000, ActiveSkill::ShouldSucceedOnAnimationFailure
        IGNORED_BY_PETS = 1 << 15, // 0x00008000, ActiveSkill::IsIgnoredByPets
        IGNORE_WEAPON_FOCUS = 1 << 16, // 0x00010000, ActiveSkill::GetIgnoreWeaponFocus
        IGNORE_SHIELD_FOCUS = 1 << 17, // 0x00020000, ActiveSkill::GetIgnoreShieldFocus
        IGNORE_RESET_TIMERS = 1 << 18, // 0x00040000, ActiveSkill::GetIgnoreResetTimers
        USES_EFFECT_REQUIRED_ARMOR_MODIFIER = 1 << 19, // 0x00080000, ActiveSkill::UsesEffectRequiredArmorModifier
        HEAL_ONLY = 1 << 20, // 0x00100000, ActiveSkill::IsHealOnly
    }
}
