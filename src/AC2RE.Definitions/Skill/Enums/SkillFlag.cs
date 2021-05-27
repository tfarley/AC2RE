using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum SkillFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        UNTRAINABLE = 1 << 0, // 0x00000001, Skill::GetUntrainable
        HIDDEN = 1 << 1, // 0x00000002, Skill::GetHidden
        CANNOT_RAISE = 1 << 2, // 0x00000004, Skill::IsCannotRaise
        NOT_TRAINABLE_BY_PLAYER = 1 << 3, // 0x00000008, Skill::IsNotTrainableByPlayer
        HERO_SKILL = 1 << 4, // 0x00000010, Skill::IsHeroSkill
        TOGGLED = 1 << 5, // 0x00000020, Skill::IsToggleSkill
        ZERO_VIGOR_PENALTY = 1 << 6, // 0x00000040, Skill::HasZeroVigorPenalty
    }
}
