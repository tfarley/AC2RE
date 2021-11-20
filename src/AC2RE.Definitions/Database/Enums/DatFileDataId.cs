namespace AC2RE.Definitions {

    // Enum DatFileDataID
    public enum DatFileDataId : uint {
        Undef = 0,

        First = 0xFFFF0000, // DatFileDataID_First
        IterationList = 0xFFFF0001, // DatFileDataID_IterationList
        PackVerList = 0xFFFF0002, // DatFileDataID_PackVerList
    }
}
