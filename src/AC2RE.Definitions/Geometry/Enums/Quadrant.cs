using System;

namespace AC2RE.Definitions {

    // Enum Quadrant
    [Flags]
    public enum Quadrant : uint {
        NO_HIT = 0,
        HIGH = 1 << 0, // 0x00000001
        MEDIUM = 1 << 1, // 0x00000002
        LOW = 1 << 2, // 0x00000004
        LEFT = 1 << 3, // 0x00000008
        RIGHT = 1 << 4, // 0x00000010
        FRONT = 1 << 5, // 0x00000020
        BACK = 1 << 6, // 0x00000040
    }
}
