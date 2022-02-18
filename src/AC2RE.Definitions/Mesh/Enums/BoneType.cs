namespace AC2RE.Definitions;

// Dat file 2300001B (internal) / Dat file 2300004C (internal)
public enum BoneType : uint {
    Default = 0, // _ / Default

    Camera_Target = 0x40000001, // _ / Camera_Target
    Camera_Origin = 0x40000002, // _ / Camera_Origin
    neck = 0x40000003, // _ / neck
    back = 0x40000004, // _ / back
    head = 0x40000005, // _ / head
    midback = 0x40000006, // _ / midback

    eyes = 0x41000001, // eyes

    neckmid = 0x81000001, // neckmid
    rweaponholder = 0x81000002, // rweaponholder
    lweaponholder = 0x81000003, // lweaponholder

    root = 0x81000005, // root
    hipgirdle = 0x81000006, // hipgirdle
    lowerback = 0x81000007, // lowerback
    lumbar = 0x81000008, // lumbar

    thorax = 0x8100000B, // thorax
    rfemur = 0x8100000C, // rfemur
    lfemur = 0x8100000D, // lfemur
    rknee = 0x8100000E, // rknee
    lknee = 0x8100000F, // lknee
    rankle = 0x81000010, // rankle
    lankle = 0x81000011, // lankle
    rball = 0x81000012, // rball
    lball = 0x81000013, // lball
    Rcollar = 0x81000014, // Rcollar
    Lcollar = 0x81000015, // Lcollar
    Rhumerus = 0x81000016, // Rhumerus
    Lhumerus = 0x81000017, // Lhumerus
    Relbow = 0x81000018, // Relbow
    Lelbow = 0x81000019, // Lelbow
    Rulna = 0x8100001A, // Rulna
    Lulna = 0x8100001B, // Lulna
    Rwrist = 0x8100001C, // Rwrist
    Lwrist = 0x8100001D, // Lwrist
    Rpalm = 0x8100001E, // Rpalm
    Lpalm = 0x8100001F, // Lpalm
    Rfingers = 0x81000020, // Rfingers
    Lfingers = 0x81000021, // Lfingers
    Rfingers_mid = 0x81000022, // Rfingers_mid
    Lfingers_mid = 0x81000023, // Lfingers_mid
    TailBase = 0x81000024, // TailBase
    Tail1 = 0x81000025, // Tail1
    Tail2 = 0x81000026, // Tail2
    Tail3 = 0x81000027, // Tail3
    Tail4 = 0x81000028, // Tail4
    Tail5 = 0x81000029, // Tail5
    Tail6 = 0x8100002A, // Tail6
    Lshoulderguard = 0x8100002B, // Lshoulderguard
    Rshoulderguard = 0x8100002C, // Rshoulderguard
    DrumBone = 0x8100002D, // DrumBone
    Mouth = 0x8100002E, // Mouth

    Particle_Front = 0x81000031, // Particle_Front
    Particle_Back = 0x81000032, // Particle_Back
    Particle_Right = 0x81000033, // Particle_Right
    Particle_Left = 0x81000034, // Particle_Left
    Particle_Center = 0x81000035, // Particle_Center
}
