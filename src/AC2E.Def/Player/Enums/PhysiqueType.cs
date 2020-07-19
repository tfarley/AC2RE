using System;

namespace AC2E.Def {

    // Const *_PhysiqueType
    // Dat file 2300000A
    [Flags]
    public enum PhysiqueType : uint {
        UNDEF = 0,
        BUILD = 1 << 0, // 0x00000001
        HEIGHT = 1 << 1, // 0x00000002
        SKIN_TONE = 1 << 2, // 0x00000004
        SKIN_DETAIL = 1 << 3, // 0x00000008
        HEAD_DETAIL = 1 << 4, // 0x00000010
        HEAD_FRILL = 1 << 5, // 0x00000020
        FRILL_COLOR = 1 << 6, // 0x00000040
        SPECIAL = 1 << 7, // 0x00000080
        SHIRT_CLOTHING_COLOR = 1 << 8, // 0x00000100
        PANTS_CLOTHING_COLOR = 1 << 9, // 0x00000200
        BOOTS_CLOTHING_COLOR = 1 << 10, // 0x00000400
        CLOTHING_MASK = SHIRT_CLOTHING_COLOR | PANTS_CLOTHING_COLOR | BOOTS_CLOTHING_COLOR, // 0x00000700
        FACE_DETAIL = 1 << 11, // 0x00000800
        EYE_COLOR = 1 << 12, // 0x00001000
        ALL = uint.MaxValue, // 0xFFFFFFFF
    }
}
