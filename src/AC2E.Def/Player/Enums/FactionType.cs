using System;

namespace AC2E.Def {

    // Const *_FactionType
    // Dat file 230000A0
    [Flags]
    public enum FactionType : uint {
        UNDEF = 0,
        FACTION_1 = 1 << 0, // 0x00000001
        FACTION_2 = 1 << 1, // 0x00000002
        FACTION_3 = 1 << 2, // 0x00000004

        NEUTRAL = 0x80000000,
    }
}
