namespace AC2RE.Definitions;

public class SendToRoomByIdChatRequestAsyncMethod : BaseChatMethod {

    // SendToRoomByIDRequest
    public ChatAsyncMethodId methodId; // m_methodID
    public uint roomId; // m_roomID
    public string text; // m_text
    public byte[] remoteBlob; // m_remoteBlob

    public SendToRoomByIdChatRequestAsyncMethod(AC2Reader data) : base(data) {
        methodId = data.ReadEnum<ChatAsyncMethodId>();
        roomId = data.ReadUInt32();
        text = data.ReadMultiByteString();
        uint remoteBlobLength = data.ReadUInt32();
        remoteBlob = data.ReadBytes((int)remoteBlobLength);
    }
}
