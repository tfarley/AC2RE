namespace AC2RE.Definitions;

// Const *_NetAuth
public enum AuthType : uint {
    Undef = 0, // Undef_NetAuth
    AccountOnly = 1, // AccountOnly_NetAuth
    Password = 2, // Password_NetAuth

    GLSUserNamePassword = 0x40000001, // GLSUserNamePassword_NetAuthType
    GLSUserNameTicket = 0x40000002, // GLSUserNameTicket_NetAuthType
    GLSServiceProvider = 0x40000004, // GLSServiceProvider_NetAuthType

    GunTicket = 0x41000001, // GunTicket_NetAuth
}
