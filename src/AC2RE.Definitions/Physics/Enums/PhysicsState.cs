using System;

namespace AC2RE.Definitions {

    // Enum PhysicsState
    [Flags]
    public enum PhysicsState : uint {
        NONE = 0,
        STATIC = 1 << 0, // 0x00000001
        CANNOT_STAND_ON = 1 << 1, // 0x00000002
        REPORT_COLLISIONS = 1 << 2, // 0x00000004
        IGNORE_COLLISIONS = 1 << 3, // 0x00000008
        NODRAW = 1 << 4, // 0x00000010
        MISSILE = 1 << 5, // 0x00000020
        PUSHABLE = 1 << 6, // 0x00000040
        ALIGNPATH = 1 << 7, // 0x00000080
        PATHCLIPPED = 1 << 8, // 0x00000100
        GRAVITY = 1 << 9, // 0x00000200
        LIGHTING_ON = 1 << 10, // 0x00000400
        UNDETECTABLE = 1 << 11, // 0x00000800
        HAS_PHYSICS_BSP = 1 << 12, // 0x00001000
        INELASTIC = 1 << 13, // 0x00002000
        CLOAKED = 1 << 14, // 0x00004000
        EDGE_SLIDE = 1 << 15, // 0x00008000
        SLEDDING = 1 << 16, // 0x00011000
        MOVEMENT_FROZEN = 1 << 17, // 0x00021000
        ANIMATION_FROZEN = 1 << 18, // 0x00041000
        GAMEDRAW = 1 << 19, // 0x00081000
        TOOLSDRAW = 1 << 20, // 0x00101000
        MOBILE = 1 << 21, // 0x00201000

        INWORKSPACE = 1 << 28, // 0x10000000
        PHYSICSDISABLED = 1 << 29, // 0x20000000
        HAS_MATRIX_WEIGHTS = 1 << 30, // 0x40000000
        SYNC_TRANSIT = 1u << 31, // 0x80000000
    }
}
