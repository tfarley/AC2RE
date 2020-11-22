namespace AC2RE.Definitions {

    // Enum ICMDCommandEnum
    public enum ICMDCommand : uint {
        NOP = 1,
        ECHO_REQUEST = 0x71655245,
        ECHO_REPLY = 0x6C705245,
    }
}
