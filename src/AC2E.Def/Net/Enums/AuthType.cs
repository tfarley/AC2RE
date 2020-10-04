namespace AC2E.Def {

    // Const *_NetAuth
    public enum AuthType : uint {
        UNDEF = 0,
        ACCOUNT_ONLY = 0x00000001,
        PASSWORD = 0x00000002,

        GLS_USERNAME_PASSWORD = 0x40000001,
        GLS_USERNAME_TICKET = 0x40000002,
        GLS_SERVICE_PROVIDER = 0x40000004,

        GUN_TICKET = 0x41000001,
    }
}
