﻿using System;

namespace AC2RE.Definitions {

    // Const *_AppearanceKey
    // Dat file 2300001F
    [Flags]
    public enum AppearanceKey : uint {
        SKINCOLOR = 0x1,
        CLOTHINGCOLOR = 0x2,
        HEADMESH = 0x3,

        HEADCOLOR = 0xB,
        BEARDMESH = 0xC,

        CLOTHINGCOLORSECONDARY = 0xE,
        SKINTEXTURE = 0xF,
        WORN = 0x10,

        PILE = 0x41000001,
        MUSICPART = 0x41000002,
        SPECIAL = 0x41000003,
        CLOTHINGCOLORTERTIARY = 0x41000004,
        ITEMQUALITY = 0x41000005,

        DNG_SANDSTONE = 0x41000007,
        DNG_COLDSTONE = 0x41000008,
        DNG_SEASHORE = 0x41000009,
        DNG_GREYSTONE = 0x4100000A,
        DNG_CARVEROCK = 0x4100000B,
        UPKEEP = 0x4100000C,
        MUSICINSTRUMENT = 0x4100000D,
        DNG_BASIC = 0x4100000E,
        DNG_BASIC_HAUNTED = 0x4100000F,

        MOSSY = 0x41000014,
        SNOW = 0x41000015,
        SAND = 0x41000016,
        LEVEL = 0x41000017,
        DNG_ICECAVE = 0x41000018,

        WEAPONEFFECT = 0x4100001A,
        HIT_SOUND = 0x4100001B,
        FACETEXTURE = 0x4100001C,
        DNG_SANDSTONE_GRASS = 0x4100001D,
        DNG_SEASHORE_SWAMP = 0x4100001E,
        DNG_BASIC_EMPLITE = 0x4100001F,
        HEADSPECIAL = 0x41000020,
        DECAL = 0x41000021,
        MOUNTSKIN = 0x41000022,
        DNG_VOLCANIC = 0x41000023,
        HULKSKIN = 0x41000024,

        DNG_OLTHOI = 0x41000026,
        HELMCOLOR = 0x41000027,

        DNG_VOLCANIC2 = 0x41000029,
        DNG_CHAOTIC = 0x4100002A,
        DNG_CASTLE_HUMAN = 0x4100002B,
        DNG_FANCY_BROKEN = 0x4100002C,
        DNG_CAVE_RED = 0x4100002D,
        EYES = 0x4100002E,
        DNG_CAVE_DARK = 0x4100002F,

        ITEMEFFECT = 0x81000001,
    }
}
