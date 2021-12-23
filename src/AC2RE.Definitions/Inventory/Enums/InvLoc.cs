using System;

namespace AC2RE.Definitions;

// Dat file 23000074 / Const *_InvLoc
[Flags]
public enum InvLoc : uint {
    None = 0, // _ / None_InvLoc
    Head = 1 << 0, // Head / Head_InvLoc 0x00000001
    Torso = 1 << 1, // Torso / Torso_InvLoc 0x00000002
    Back = 1 << 2, // Back / Back_InvLoc 0x00000004
    Legs = 1 << 3, // Legs / Legs_InvLoc 0x00000008
    Feet = 1 << 4, // Feet / Feet_InvLoc 0x00000010
    Hands = 1 << 5, // Hands / Hands_InvLoc 0x00000020
    PrimaryHand = 1 << 6, // PrimaryHand / PrimaryHand_InvLoc 0x00000040
    SecondaryHand = 1 << 7, // SecondaryHand / SecondaryHand_InvLoc 0x00000080
    PrimaryRing = 1 << 8, // PrimaryRing / PrimaryRing_InvLoc 0x00000100
    SecondaryRing = 1 << 9, // SecondaryRing / SecondaryRing_InvLoc 0x00000200
    Necklace = 1 << 10, // Necklace / Necklace_InvLoc 0x00000400
}
