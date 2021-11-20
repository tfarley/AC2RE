using System;

namespace AC2RE.Definitions {

    // Dat file 23000007 / Const *_SpeciesType
    [Flags]
    public enum SpeciesType : uint {
        Undef = 0, // _ / Undef_SpeciesType
        Human = 1 << 0, // Human / Human_SpeciesType 0x00000001
        Lugian = 1 << 1, // Lugian / Lugian_SpeciesType 0x00000002
        Tumerok = 1 << 2, // Tumerok / Tumerok_SpeciesType 0x00000004
        Drudge = 1 << 3, // Drudge / Mosswart_SpeciesType 0x00000008
        Empyrean = 1 << 4, // Empyrean / _ 0x00000010

        Any = uint.MaxValue, // Any / Any_SpeciesType
    }
}
