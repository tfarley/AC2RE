using System;

namespace AC2RE.Definitions {

    // Enum ObjectInfoEnum
    [Flags]
    public enum ObjectInfo : uint {
        DEFAULT = 0,
        CONTACT = 1 << 0, // 0x00000001
        ON_WALKABLE = 1 << 1, // 0x00000002
        IS_VIEWER = 1 << 2, // 0x00000004
        PATH_CLIPPED = 1 << 3, // 0x00000008
        FREE_ROTATE = 1 << 4, // 0x00000010

        PERFECT_CLIP = 1 << 6, // 0x00000040
        EDGE_SLIDE = 1 << 7, // 0x00000080
        IS_PLAYER = 1 << 8, // 0x00000100
        PREDETERMINED_TRANSITION = 1 << 9, // 0x00000200
    }
}
