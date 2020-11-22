using System;

namespace AC2RE.Definitions {

    // Const *_ClassType
    // Dat file 23000008
    [Flags]
    public enum ClassType : uint {
        UNDEF = 0,
        WARRIOR = 1 << 0, // 0x00000001
        ARCHER = 1 << 1, // 0x00000002
        SHAMAN = 1 << 2, // 0x00000004
        EXARCH = 1 << 3, // 0x00000008
        FANATIC = 1 << 4, // 0x00000010
    }
}
