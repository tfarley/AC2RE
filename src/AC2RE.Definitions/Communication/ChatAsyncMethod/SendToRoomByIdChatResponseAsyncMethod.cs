namespace AC2RE.Definitions;

public class SendToRoomByIdChatResponseAsyncMethod : BaseChatMethod {

    // SendToRoomByIDResponse
    public ChatAsyncMethodId methodId; // m_methodID
    public uint result; // m_hResult

    public SendToRoomByIdChatResponseAsyncMethod() {

    }

    public SendToRoomByIdChatResponseAsyncMethod(AC2Reader data) : base(data) {
        methodId = data.ReadEnum<ChatAsyncMethodId>();
        result = data.ReadUInt32();
    }

    public override void write(AC2Writer data) {
        base.write(data);
        data.WriteEnum(methodId);
        data.Write(result);
    }
}
