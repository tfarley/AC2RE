namespace AC2RE.Definitions;

// Dat file 2300001A / Const *_HoldingLocationID
public enum HoldingLocation : uint {
    Invalid, // _ / Invalid_HoldingLocationID
    RHand, // _ / RHand_HoldingLocationID
    LHand, // _ / LHand_HoldingLocationID
    Drum, // Drum / Drum_HoldingLocationID
    Arrow, // Arrow / Arrow_HoldingLocationID
    Particle_Front, // Particle_Front / Particle_Front_HoldingLocationID
    Particle_Back, // Particle_Back / Particle_Back_HoldingLocationID
    Particle_Right, // Particle_Right / Particle_Right_HoldingLocationID
    Particle_Left, // Particle_Left / Particle_Left_HoldingLocationID
    Particle_Center, // Particle_Center / Particle_Center_HoldingLocationID

    Arrow_Offhand = 16, // Arrow_Offhand / Arrow_Offhand_HoldingLocationID
    LHand2 = 17, // _ / LHand2_HoldingLocationID
    RHand2 = 18, // _ / RHand2_HoldingLocationID
    LHand3 = 19, // _ / LHand3_HoldingLocationID

    Orb_Left = 32, // Orb_Left / _
    Orb_Right = 48, // Orb_Right / _
    Celestrum = 64, // Celestrum / _
    Projectile = 80, // Projectile / _

    origin = 0x40000003, // _ / origin_HoldingLocationID
    Camera_Origin = 0x40000004, // _ / Camera_Origin_HoldingLocationID
    Camera_Target = 0x40000005, // _ / Camera_Target_HoldingLocationID
    LHand4 = 0x40000006, // _ / LHand4_HoldingLocationID
    Cloak = 0x40000007, // _ / Cloak_HoldingLocationID

    MALELUGIAN = 0x81000001, // MALELUGIAN / _
    MALEHUMAN = 0x81000002, // MALEHUMAN / _
    MALETUMEROK = 0x81000003, // MALETUMEROK / _
    FEMALELUGIAN = 0x81000004, // FEMALELUGIAN / _
    FEMALEHUMAN = 0x81000005, // FEMALEHUMAN / _
    FEMALETUMEROK = 0x81000006, // FEMALETUMEROK / _
    CharGenCustom = 0x81000007, // CharGenCustom / _

    MALEEMPYREAN = 0x8100000A, // MALEEMPYREAN / _
    FEMALEEMPYREAN = 0x8100000B, // FEMALEEMPYREAN / _

    MALEDRUDGE = 0x8100000E, // MALEDRUDGE / _
    FEMALEDRUDGE = 0x8100000F, // FEMALEDRUDGE / _
}
