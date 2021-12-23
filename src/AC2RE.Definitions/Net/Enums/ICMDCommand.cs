namespace AC2RE.Definitions;

// Enum ICMDCommandEnum
public enum ICMDCommand : uint {
    Undef = 0,
    NOP = 1, // cmdNOP

    EchoRequest = 0x71655245, // cmdEchoRequest
    EchoReply = 0x6C705245, // cmdEchoReply
}
