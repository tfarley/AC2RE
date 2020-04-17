using System;

namespace AC2E.Def.Enums {

    // Dat file 23000007
    [Flags]
    public enum SpeciesType : uint {
        UNDEF = 0,
        HUMAN = 1 << 0,
        LUGIAN = 1 << 1,
        TUMEROK = 1 << 2,
        MOSSWART = 1 << 3,
        EMPYREAN = 1 << 4,
        ANY = uint.MaxValue,
    }
}