using System;

namespace AC2E.Def {

    // Const *_InvLoc
    [Flags]
    public enum InvLoc : uint {
        NONE = 0,
        HEAD = 1 << 0, // 0x00000001
        TORSO = 1 << 1, // 0x00000002
        BACK = 1 << 2, // 0x00000004
        LEGS = 1 << 3, // 0x00000008
        FEET = 1 << 4, // 0x00000010
        HANDS = 1 << 5, // 0x00000020
        PRIMARY_HAND = 1 << 6, // 0x00000040
        SECONDARY_HAND = 1 << 7, // 0x00000080
        PRIMARY_RING = 1 << 8, // 0x00000100
        SECONDARY_RING = 1 << 9, // 0x00000200
        NECKLACE = 1 << 10, // 0x00000400
    }
}
