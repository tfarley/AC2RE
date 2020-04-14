using System;

// DB table 2300000A
[Flags]
public enum PhysiqueType : uint {
    UNDEF = 0,
    BUILD = 1 << 0,
    HEIGHT = 1 << 1,
    SKIN_TONE = 1 << 2,
    SKIN_DETAIL = 1 << 3,
    HEAD_DETAIL = 1 << 4,
    HEAD_FRILL = 1 << 5,
    FRILL_COLOR = 1 << 6,
    SPECIAL = 1 << 7,
    SHIRT_CLOTHING_COLOR = 1 << 8,
    PANTS_CLOTHING_COLOR = 1 << 9,
    BOOTS_CLOTHING_COLOR = 1 << 10,
    CLOTHING_MASK = SHIRT_CLOTHING_COLOR | PANTS_CLOTHING_COLOR | BOOTS_CLOTHING_COLOR,
    FACE_DETAIL = 1 << 11,
    EYE_COLOR = 1 << 12,
    ALL = uint.MaxValue,
}
