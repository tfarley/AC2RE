using System;

namespace AC2RE.Definitions;

// Dat file 2300010E (internal)
[Flags]
public enum AIPetClass : uint {
    None = 0,
    Dominion = 1 << 0, // Dominion 0x00000001
    Order = 1 << 1, // Order 0x00000002
    Shadow = 1 << 2, // Shadow 0x00000004
    Kingdom = (Dominion | Order | Shadow), // Kingdom 7
    Enchanter = 1 << 3, // Enchanter 0x00000008
    RangerMobile = 1 << 4, // RangerMobile 0x00000010
    RangerTrap = 1 << 5, // RangerTrap 0x00000020
    Ranger = (RangerMobile | RangerTrap), // Ranger 48
    ElementalistSand = 1 << 6, // ElementalistSand 0x00000040
    ElementalistStationary = 1 << 7, // ElementalistStationary 0x00000080
    TacticianTurret = 1 << 8, // TacticianTurret 0x00000100
    TacticianWall = 1 << 9, // TacticianWall 0x00000200
    Tactician = (TacticianTurret | TacticianWall), // Tactician 768
    InvokerGrandMother = 1 << 10, // InvokerGrandMother 0x00000400
    InvokerGrandFather = 1 << 11, // InvokerGrandFather 0x00000800
    FeralIntendant = 1 << 12, // FeralIntendant 0x00001000
    HiveKeeper = 1 << 13, // HiveKeeper 0x00002000
    InvokerAvenger = 1 << 14, // InvokerAvenger 0x00004000
    InvokerAvengerGrandfather = (InvokerGrandFather | InvokerAvenger), // InvokerAvengerGrandfather 18432
    InvokerReckoner = 1 << 15, // InvokerReckoner 0x00008000
    InvokerReckonerGrandfather = (InvokerGrandFather | InvokerReckoner), // InvokerReckonerGrandfather 34816
    InvokerVindicator = 1 << 16, // InvokerVindicator 0x00010000
    InvokerVindicatorGrandfather = (InvokerGrandFather | InvokerVindicator), // InvokerVindicatorGrandfather 67584
    Invoker = (InvokerGrandMother | InvokerGrandFather | InvokerAvenger | InvokerReckoner | InvokerVindicator), // Invoker 117760

    BloodOfTyrantsQuest = 1 << 24, // BloodOfTyrantsQuest 0x01000000
    DilloRustlersQuest = 1 << 25, // DilloRustlersQuest 0x02000000
    GenericQuestPet = 1 << 26, // GenericQuestPet 0x04000000
    HeroPet = 1 << 27, // HeroPet 0x08000000
    SwarmPet = 1 << 28, // SwarmPet 0x10000000
    ElementalistHighSand = 1 << 29, // ElementalistHighSand 0x20000000
    Elementalist = (ElementalistSand | ElementalistStationary | ElementalistHighSand), // Elementalist 536871104
}
