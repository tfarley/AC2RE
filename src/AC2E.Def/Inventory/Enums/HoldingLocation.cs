namespace AC2E.Def {

    // Const *_HoldingLocationID
    // Dat file 2300001A
    public enum HoldingLocation : uint {
        INVALID,
        R_HAND,
        L_HAND,
        DRUM,
        ARROW,
        PARTICLE_FRONT,
        PARTICLE_BACK,
        PARTICLE_RIGHT,
        PARTICLE_LEFT,
        PARTICLE_CENTER,

        ARROW_OFFHAND = 16,
        L_HAND2 = 17,
        R_HAND2 = 18,
        L_HAND3 = 19,

        ORIGIN = 0x40000003,
        CAMERA_ORIGIN = 0x40000004,
        CAMERA_TARGET = 0x40000005,
        L_HAND4 = 0x40000006,
        CLOAK = 0x40000007,
    }
}
