using System;

namespace AC2E.Def.Enums {

    // Enum TransientState
    [Flags]
    public enum TransientState : uint {
        NONE = 0,
        CONTACT = 1 << 0, // 0x00000001
        ON_WALKABLE = 1 << 1, // 0x00000002
        SLIDING = 1 << 2, // 0x00000004
        WATER_CONTACT = 1 << 3, // 0x00000008
        STATIONARY_FALL = 1 << 4, // 0x00000010
        STATIONARY_STOP = 1 << 5, // 0x00000020
        STATIONARY_STUCK = 1 << 6, // 0x00000040
        MOVEMENT_ACTIVE = 1 << 7, // 0x00000080
        ANIMATION_ACTIVE = 1 << 8, // 0x00000100
        MISC_ACTIVE = 1 << 9, // 0x00000200
        LANDSCAPE_CONTACT = 1 << 10, // 0x00000400
        ENVIRONMENT_CONTACT = 1 << 11, // 0x00000800
        MIGRATING = 1 << 12, // 0x00001000
        OUT_OF_PHYSICS_RANGE = 1 << 13, // 0x00002000
    }
}
