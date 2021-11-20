using System;

namespace AC2RE.Definitions {

    // Dat file 230000A0 / Const *_FactionType
    [Flags]
    public enum FactionType : uint {
        Undef = 0, // _ / Undef_FactionType
        Faction1 = 1 << 0, // Faction1 / Faction1_FactionType 0x00000001
        Faction2 = 1 << 1, // Faction2 / Faction2_FactionType 0x00000002
        Faction3 = 1 << 2, // Faction3 / Faction3_FactionType 0x00000004

        Neutral = 0x80000000, // Neutral / Neutral_FactionType
    }
}
