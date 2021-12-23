using System;

namespace AC2RE.Definitions;

// Dat file 23000003
[Flags]
public enum ImplementType : uint {
    Undef = 0,
    None = 1 << 0, // None 0x00000001
    Sword = 1 << 1, // Sword 0x00000002
    Bow = 1 << 2, // Bow 0x00000004
    Drum = 1 << 3, // Drum 0x00000008
    Shield = 1 << 4, // Shield 0x00000010
    Axe = 1 << 5, // Axe 0x00000020
    Dagger = 1 << 6, // Dagger 0x00000040
    Gunblade = 1 << 7, // Gunblade 0x00000080
    Hammer = 1 << 8, // Hammer 0x00000100
    Unarmed = 1 << 9, // Unarmed 0x00000200
    TwoHandedSword = 1 << 10, // TwoHandedSword 0x00000400
    AlchemyKit = 1 << 11, // AlchemyKit 0x00000800
    Boulder = 1 << 12, // Boulder 0x00001000
    BeeHive = 1 << 13, // BeeHive 0x00002000
    TurretConstructionKit = 1 << 14, // TurretConstructionKit 0x00004000
    Spear = 1 << 15, // Spear 0x00008000
    Jailai = 1 << 16, // Jailai 0x00010000
    Flail = 1 << 17, // Flail 0x00020000
    Stave = 1 << 18, // Stave 0x00040000
    Scythe = 1 << 19, // Scythe + OneHanded 0x00080000
    Hand = 1 << 20, // Hand 0x00100000
    Orb = 1 << 21, // Orb 0x00200000

    Weapon = Sword | Bow | Drum | Axe | Dagger | Gunblade | Hammer | Unarmed | TwoHandedSword | AlchemyKit | Boulder | BeeHive | TurretConstructionKit | Spear | Jailai | Flail | Stave | Scythe | Hand | Orb, // Weapon 0x003FFFEE

    Natural = 1u << 31, // Natural 0x00200000

    AnyButNatural = AnyAndNone & ~(None | Natural), // AnyButNatural 0x7FFFFFFE
    NaturalAndNone = None & Natural, // NaturalAndNone 0x80000001
    AnyButNone = AnyAndNone & ~None, // AnyButNone 0x80000001

    AnyAndNone = uint.MaxValue, // AnyAndNone
}
