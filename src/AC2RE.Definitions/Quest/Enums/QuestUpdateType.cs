namespace AC2RE.Definitions;

// Dat file 23000092 (internal)
public enum QuestUpdateType : uint {
    Undef = 0,

    PhaseNumberMask = 268435455, // PhaseNumberMask

    Bestow = 268435456, // Bestow
    Advance = 536870912, // Advance
    Advance_Phase2 = 536870914, // Advance_Phase2
    Advance_Phase3 = 536870915, // Advance_Phase3
    Advance_Phase4 = 536870916, // Advance_Phase4
    Advance_Phase5 = 536870917, // Advance_Phase5
    Advance_Phase6 = 536870918, // Advance_Phase6
    Advance_Phase7 = 536870919, // Advance_Phase7
    Advance_Phase8 = 536870920, // Advance_Phase8
    Advance_Phase9 = 536870921, // Advance_Phase9
    Advance_Phase10 = 536870922, // Advance_Phase10
    Advance_Phase11 = 536870923, // Advance_Phase11
    Advance_Phase12 = 536870924, // Advance_Phase12
    Advance_Phase13 = 536870925, // Advance_Phase13
    Advance_Phase14 = 536870926, // Advance_Phase14
    Advance_Phase15 = 536870927, // Advance_Phase15
    Advance_Phase16 = 536870928, // Advance_Phase16
    Advance_Phase17 = 536870929, // Advance_Phase17
    Advance_Phase18 = 536870930, // Advance_Phase18
    Advance_Phase19 = 536870931, // Advance_Phase19
    Advance_Phase20 = 536870932, // Advance_Phase20
    Advance_Phase21 = 536870933, // Advance_Phase21
    Advance_Phase22 = 536870934, // Advance_Phase22
    Advance_Phase23 = 536870935, // Advance_Phase23
    Advance_Phase24 = 536870936, // Advance_Phase24
    Advance_Phase25 = 536870937, // Advance_Phase25
    Advance_Phase26 = 536870938, // NOTE: guessed
    Complete = 805306368, // Complete
    Fail = 1073741824, // Fail
    Remove = 1342177280, // Remove
    Increment = 1610612736, // Increment

    RawUpdateTypeMask = 4026531840, // RawUpdateTypeMask
}
