namespace AC2E.Def {

    // Enum CharGenResponseEnum
    public enum CharGenResponse : uint {
        UNDEF = 0,
        OK = 1,
        PENDING = 2,
        NAME_IN_USE = 3,
        NAME_BANNED = 4,
        CORRUPT = 5,
        DATABASE_DOWN = 6,
        ADMIN_PRIVILEGE_DENIED = 7,
        NO_MORE_SLOTS = 8,
    }
}
