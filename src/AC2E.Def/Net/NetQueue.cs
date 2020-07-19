namespace AC2E.Def {

    // Const NET_QUEUE_*
    public enum NetQueue : ushort {
        INVALID = 0,
        EVENT = 1,
        CONTROL = 2,
        WEENIE = 3,
        LOGON = 4,
        DATABASE = 5,
        SECURECONTROL = 6,
        SECUREWEENIE = 7,
        SECURELOGON = 8,
        MAX = 9,
    }
}
