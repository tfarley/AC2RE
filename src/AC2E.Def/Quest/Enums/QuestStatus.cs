namespace AC2E.Def {

    // Dat file 23000091
    public enum QuestStatus : uint {
        UNDERWAY = 0x10000000,
        FAILED = 0x20000000,
        COMPLETED = 0x30000000,
        NOTUNDERWAY = 0x40000000,
        CANBEBESTOWED = 0x50000000,
        CANCELLED = 0x80000000,
    }
}
