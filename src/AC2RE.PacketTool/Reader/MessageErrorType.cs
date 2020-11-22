namespace AC2RE.PacketTool {

    public enum MessageErrorType : uint {
        UNDETERMINED,
        NONE,
        PARTIAL_READ,
        INCOMPLETE_BLOB,
        UNHANDLED_OPCODE,
        NOT_IMPLEMENTED,
        PARSE_FAILURE,
    }
}
