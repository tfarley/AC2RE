namespace AC2E.Dat {

    // Enum NodeNameType
    public enum NodeNameType : byte {
        PSTRING = 0,
        UINT32 = 1,
        INT32 = 2,
        DOUBLE = 3,
        FLOAT = 4,
        CELLID = 5,
        INSTANCEID = 6,
        BOOL = 7,
        HEX = 8,
        BINARY = 9,
        BLOCKID = 10,
        UINT64 = 11,
        INT64 = 12,
        LONGHEX = 13,
        ENUM = 14,
        NULL = 255,
    }
}
