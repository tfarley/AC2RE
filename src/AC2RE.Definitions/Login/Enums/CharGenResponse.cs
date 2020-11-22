namespace AC2RE.Definitions {

    // Enum CharGenResponseEnum
    public enum CharGenResponse : uint {
        UNDEF,
        OK,
        PENDING,
        NAME_IN_USE,
        NAME_BANNED,
        CORRUPT,
        DATABASE_DOWN,
        ADMIN_PRIVILEGE_DENIED,
        NO_MORE_SLOTS,
    }
}
