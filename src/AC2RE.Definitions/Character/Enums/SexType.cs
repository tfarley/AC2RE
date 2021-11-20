using System;

namespace AC2RE.Definitions {

    // Dat file 23000009 / Const *_SexType
    [Flags]
    public enum SexType : uint {
        Undef = 0, // _ / Undef_SexType

        Male = 1 << 12, // Male / Male_SexType  0x00001000
        Female = 1 << 13, // Female / Female_SexType 0x00002000
    }
}
