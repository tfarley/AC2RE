﻿using System;

namespace AC2RE.Definitions {

    // Enum ObjectInfoEnum
    [Flags]
    public enum ObjectInfo : uint {
        DEFAULT = 0, // DEFAULT_OI
        CONTACT = 1 << 0, // CONTACT_OI 0x00000001
        ON_WALKABLE = 1 << 1, // ON_WALKABLE_OI 0x00000002
        IS_VIEWER = 1 << 2, // IS_VIEWER_OI 0x00000004
        PATH_CLIPPED = 1 << 3, // PATH_CLIPPED_OI 0x00000008
        FREE_ROTATE = 1 << 4, // FREE_ROTATE_OI 0x00000010

        PERFECT_CLIP = 1 << 6, // PERFECT_CLIP_OI 0x00000040
        EDGE_SLIDE = 1 << 7, // EDGE_SLIDE_OI 0x00000080
        IS_PLAYER = 1 << 8, // IS_PLAYER_OI 0x00000100
        PREDETERMINED_TRANSITION = 1 << 9, // PREDETERMINED_TRANSITION_OI 0x00000200
    }
}
