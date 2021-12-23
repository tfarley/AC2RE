using System;

namespace AC2RE.Definitions;

// Enum Quadrant
[Flags]
public enum Quadrant : uint {
    NO_HIT = 0, // HQ_NO_HIT
    HIGH = 1 << 0, // HQ_HIGH 0x00000001
    MEDIUM = 1 << 1, // HQ_MEDIUM 0x00000002
    LOW = 1 << 2, // HQ_LOW 0x00000004
    LEFT = 1 << 3, // HQ_LEFT 0x00000008
    RIGHT = 1 << 4, // HQ_RIGHT 0x00000010
    FRONT = 1 << 5, // HQ_FRONT 0x00000020
    BACK = 1 << 6, // HQ_BACK 0x00000040
}
