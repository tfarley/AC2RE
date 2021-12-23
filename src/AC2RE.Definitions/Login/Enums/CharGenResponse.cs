namespace AC2RE.Definitions;

// Enum CharGenResponseEnum
public enum CharGenResponse : uint {
    Undef, // Undef_CharGenResponse
    Ok, // Ok_CharGenResponse
    Pending, // Pending_CharGenResponse
    NameInUse, // NameInUse_CharGenResponse
    NameBanned, // NameBanned_CharGenResponse
    Corrupt, // Corrupt_CharGenResponse
    DatabaseDown, // DatabaseDown_CharGenResponse
    AdminPrivilegeDenied, // AdminPrivilegeDenied_CharGenResponse
    NoMoreSlots, // NoMoreSlots_CharGenResponse
    Num, // Num_CharGenResponse
}
