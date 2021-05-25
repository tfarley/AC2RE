using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum SkillInfoMask : uint {
        NONE = 0,
        TRAINED = 1 << 0, // 0x00000001, SkillInfo::IsTrained
        PERSONAL_UNTRAINABLE = 1 << 1, // 0x00000002, SkillInfo::IsPersonalUntrainable

        BASE_MANEUVER = 1 << 3, // 0x00000008, SkillInfo::IsBaseManeuver
        CANNOT_RAISE = 1 << 4, // 0x00000010, SkillInfo::IsCannotRaise
        HAS_LAST_TIME_USED = 1 << 5, // 0x00000020, SkillInfo::HasTimeLastUsed
        HAS_TIME_GRANTED = 1 << 6, // 0x00000040, SkillInfo::HasTimeGranted
        TOGGLED = 1 << 7, // 0x00000080, SkillInfo::IsToggled
    }
}
