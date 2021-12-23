namespace AC2RE.Definitions;

// Dat file 230000AF
public enum AIMovementType : uint {
    Undef = 0,

    Stationary = 0x40000001, // Stationary
    TurnOnly = 0x40000002, // TurnOnly
    ConstrainedToHome = 0x40000003, // ConstrainedToHome
    ConstrainedToMaster = 0x40000004, // ConstrainedToMaster
    FreeRange = 0x40000005, // FreeRange
}
