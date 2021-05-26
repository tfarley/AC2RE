using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum PerkSkillFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        VITAL_CHANGE = 1 << 0, // 0x00000001, PerkSkill::IsVitalChange
    }
}
