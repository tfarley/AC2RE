using System;

namespace AC2E.Def {

    // Dat file 230000A0
    [Flags]
    public enum FactionType : uint {
        FACTION_1 = 0x1,
        FACTION_2 = 0x2,
        FACTION_3 = 0x4,
        NEUTRAL = 0x80000000,
    }
}
