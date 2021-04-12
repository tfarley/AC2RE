namespace AC2RE.Definitions {

    // Dat file 230000AF
    public enum AIMovementType : uint {
        STATIONARY = 0x40000001,
        TURN_ONLY = 0x40000002,
        CONSTRAINED_TO_HOME = 0x40000003,
        CONSTRAINED_TO_MASTER = 0x40000004,
        FREE_RANGE = 0x40000005,
    }
}
