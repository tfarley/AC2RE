using System;

namespace AC2RE.Definitions {

    // Const *_SexType
    // Dat file 23000009
    [Flags]
    public enum SexType : uint {
        UNDEF = 0,

        MALE = 1 << 12, // 0x00001000
        FEMALE = 1 << 13, // 0x00002000
    }
}
