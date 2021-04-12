using System;

namespace AC2RE.Definitions {

    // Dat file 23000003
    [Flags]
    public enum ImplementType : uint {
        NONE = 1 << 0, // 0x00000001
        SWORD = 1 << 1, // 0x00000002
        BOW = 1 << 2, // 0x00000004
        DRUM = 1 << 3, // 0x00000008
        SHIELD = 1 << 4, // 0x00000010
        AXE = 1 << 5, // 0x00000020
        DAGGER = 1 << 6, // 0x00000040
        GUNBLADE = 1 << 7, // 0x00000080
        HAMMER = 1 << 8, // 0x00000100
        UNARMED = 1 << 9, // 0x00000200
        TWO_HANDED_SWORD = 1 << 10, // 0x00000400
        ALCHEMY_KIT = 1 << 11, // 0x00000800
        BOULDER = 1 << 12, // 0x00001000
        BEE_HIVE = 1 << 13, // 0x00002000
        TURRET_CONSTRUCTION_KIT = 1 << 14, // 0x00004000
        SPEAR = 1 << 15, // 0x00008000
        JAILAI = 1 << 16, // 0x00010000
        FLAIL = 1 << 17, // 0x00020000
        STAVE = 1 << 18, // 0x00040000
        SCYTHE = 1 << 19, // 0x00080000
        HAND = 1 << 20, // 0x00100000
        ORB = 1 << 21, // 0x00200000
    }
}
