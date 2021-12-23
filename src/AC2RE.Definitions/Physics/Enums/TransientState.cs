﻿using System;

namespace AC2RE.Definitions;

// Enum TransientState
[Flags]
public enum TransientState : uint {
    NONE = 0,
    CONTACT = 1 << 0, // CONTACT_TS 0x00000001
    ON_WALKABLE = 1 << 1, // ON_WALKABLE_TS 0x00000002
    SLIDING = 1 << 2, // SLIDING_TS 0x00000004
    WATER_CONTACT = 1 << 3, // WATER_CONTACT_TS 0x00000008
    STATIONARY_FALL = 1 << 4, // STATIONARY_FALL_TS 0x00000010
    STATIONARY_STOP = 1 << 5, // STATIONARY_STOP_TS 0x00000020
    STATIONARY_STUCK = 1 << 6, // STATIONARY_STUCK_TS 0x00000040
    MOVEMENT_ACTIVE = 1 << 7, // MOVEMENT_ACTIVE_TS 0x00000080
    ANIMATION_ACTIVE = 1 << 8, // ANIMATION_ACTIVE_TS 0x00000100
    MISC_ACTIVE = 1 << 9, // MISC_ACTIVE_TS 0x00000200
    LANDSCAPE_CONTACT = 1 << 10, // LANDSCAPE_CONTACT_TS 0x00000400
    ENVIRONMENT_CONTACT = 1 << 11, // ENVIRONMENT_CONTACT_TS 0x00000800
    MIGRATING = 1 << 12, // MIGRATING_TS 0x00001000
    OUT_OF_PHYSICS_RANGE = 1 << 13, // OUT_OF_PHYSICS_RANGE_TS 0x00002000
}
