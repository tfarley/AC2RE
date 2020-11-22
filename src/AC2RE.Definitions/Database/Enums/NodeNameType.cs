namespace AC2RE.Definitions {

    // Enum NodeNameType
    public enum NodeNameType : byte {
        PSTRING,
        UINT32,
        INT32,
        DOUBLE,
        FLOAT,
        CELLID,
        INSTANCEID,
        BOOL,
        HEX,
        BINARY,
        BLOCKID,
        UINT64,
        INT64,
        LONGHEX,
        ENUM,

        NULL = 255,
    }
}
