namespace AC2RE.Definitions {

    // Enum NodeNameType
    public enum NodeNameType : byte {
        PString, // PString_NAMETYPE
        UInt32, // UInt32_NAMETYPE
        Int32, // Int32_NAMETYPE
        Double, // Double_NAMETYPE
        Float, // Float_NAMETYPE
        CellID, // CellID_NAMETYPE
        InstanceID, // InstanceID_NAMETYPE
        Bool, // Bool_NAMETYPE
        Hex, // Hex_NAMETYPE
        Binary, // Binary_NAMETYPE
        BlockID, // BlockID_NAMETYPE
        UInt64, // UInt64_NAMETYPE
        Int64, // Int64_NAMETYPE
        LongHex, // LongHex_NAMETYPE
        Enum, // Enum_NAMETYPE

        Null = 255, // Null_NAMETYPE
    }
}
