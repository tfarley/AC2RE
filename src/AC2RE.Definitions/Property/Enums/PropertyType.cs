namespace AC2RE.Definitions {

    // Const *_PropertyType
    public enum PropertyType : uint {
        Invalid = 0, // Invalid_PropertyType

        Bool = 0x40000001, // Bool_PropertyType
        Integer = 0x40000002, // Integer_PropertyType
        Float = 0x40000003, // Float_PropertyType
        Vector = 0x40000004, // Vector_PropertyType
        Color = 0x40000005, // Color_PropertyType
        String = 0x40000006, // String_PropertyType
        Enum = 0x40000007, // Enum_PropertyType
        DataFile = 0x40000008, // DataFile_PropertyType
        Waveform = 0x40000009, // Waveform_PropertyType
        StringInfo = 0x4000000A, // StringInfo_PropertyType
        PackageID = 0x4000000B, // PackageID_PropertyType

        LongInteger = 0x4000000D, // LongInteger_PropertyType
        Position = 0x4000000E, // Position_PropertyType
    }
}
