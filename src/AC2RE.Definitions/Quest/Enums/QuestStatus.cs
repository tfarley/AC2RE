namespace AC2RE.Definitions {

    // Dat file 23000091
    public enum QuestStatus : uint {
        Undef = 0,

        PhaseNumberMask = 0x0FFFFFFF, // PhaseNumberMask

        Underway = 0x10000000, // Underway
        Underway_Phase01 = 0x10000001, // Underway_Phase01
        Underway_Phase02 = 0x10000002, // Underway_Phase02
        Underway_Phase03 = 0x10000003, // Underway_Phase03
        Underway_Phase04 = 0x10000004, // Underway_Phase04
        Underway_Phase05 = 0x10000005, // Underway_Phase05
        Underway_Phase06 = 0x10000006, // Underway_Phase06
        Underway_Phase07 = 0x10000007, // Underway_Phase07
        Underway_Phase08 = 0x10000008, // Underway_Phase08
        Underway_Phase09 = 0x10000009, // Underway_Phase09
        Underway_Phase10 = 0x1000000A, // Underway_Phase10
        Underway_Phase11 = 0x1000000B, // Underway_Phase11
        Underway_Phase12 = 0x1000000C, // Underway_Phase12
        Underway_Phase13 = 0x1000000D, // Underway_Phase13
        Underway_Phase14 = 0x1000000E, // Underway_Phase14
        Underway_Phase15 = 0x1000000F, // Underway_Phase15
        Underway_Phase16 = 0x10000010, // Underway_Phase16
        Underway_Phase17 = 0x10000011, // Underway_Phase17
        Underway_Phase18 = 0x10000012, // Underway_Phase18
        Underway_Phase19 = 0x10000013, // Underway_Phase19
        Underway_Phase20 = 0x10000014, // Underway_Phase20

        Failed = 0x20000000, // Failed
        Completed = 0x30000000, // Completed
        NotUnderway = 0x40000000, // NotUnderway
        CanBeBestowed = 0x50000000, // CanBeBestowed
        Cancelled = 0x80000000, // Cancelled

        RawStatusMask = 0xF0000000, // RawStatusMask
    }
}
