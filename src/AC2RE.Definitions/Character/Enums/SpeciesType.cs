using System;

namespace AC2RE.Definitions {

    // Const *_SpeciesType
    // Dat file 23000007
    [Flags]
    public enum SpeciesType : uint {
        UNDEF = 0,
        ALL = uint.MaxValue,

        HUMAN = 1 << 0, // 0x00000001
        LUGIAN = 1 << 1, // 0x00000002
        TUMEROK = 1 << 2, // 0x00000004
        MOSSWART = 1 << 3, // 0x00000008
        EMPYREAN = 1 << 4, // 0x00000010
    }
}
