namespace AC2RE.Definitions;

public class SendToRoomChatEvent : IChatAsyncMethod {

    // SendToRoomChatEvent
    public uint roomId; // m_roomID
    public string sourceDisplayName; // m_wcsSourceDisplayName
    public string text; // m_wcsText
    public byte[] remoteBlob; // m_remoteBlob

    public SendToRoomChatEvent() {

    }

    public SendToRoomChatEvent(AC2Reader data) {
        roomId = data.ReadUInt32();
        sourceDisplayName = data.ReadMultiByteString();
        text = data.ReadMultiByteString();
        uint remoteBlobLength = data.ReadUInt32();
        remoteBlob = data.ReadBytes((int)remoteBlobLength);
    }

    public void write(AC2Writer data) {
        data.Write(roomId);
        data.WriteMultiByteString(sourceDisplayName);
        data.WriteMultiByteString(text);
        data.Write(remoteBlob.Length);
        data.Write(remoteBlob);
    }
}
