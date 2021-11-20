using System;

namespace AC2RE.Definitions {

    // Dat file 23000008 / Const *_ClassType
    [Flags]
    public enum ClassType : uint {
        Undef = 0, // _ / Undef_ClassType
        Warrior = 1 << 0, // Warrior / Warrior_ClassType 0x00000001
        Archer = 1 << 1, // Archer / Archer_ClassType 0x00000002
        Shaman = 1 << 2, // Shaman / Shaman_ClassType 0x00000004
        Exarch = 1 << 3, // Exarch / _ 0x00000008
        Fanatic = 1 << 4, // Fanatic / _ 0x00000010
    }
}
