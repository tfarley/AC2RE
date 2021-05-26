using System;

namespace AC2RE.Definitions {

    // Const *_FactionType
    // Dat file 230000A0
    [Flags]
    public enum FactionType : uint {
        UNDEF = 0,
        ALL = uint.MaxValue,

        FACTION_1 = 1 << 0, // 0x00000001
        FACTION_2 = 1 << 1, // 0x00000002
        FACTION_3 = 1 << 2, // 0x00000004

        NEUTRAL = 0x80000000,
    }
}
