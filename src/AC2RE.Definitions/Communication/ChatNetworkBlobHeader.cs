namespace AC2RE.Definitions;

public class ChatNetworkBlobHeader {

    // ChatNetworkBlobHeader
    public ChatNetworkBlobType blobType; // m_blobType
    public ChatAsyncMethodId blobDispatchType; // m_blobDispatchType
    public uint targetType; // m_targetType
    public uint targetId; // m_targetID
    public uint transportType; // m_transportType
    public uint transportId; // m_transportID
    public uint cookie; // m_cookie

    public ChatNetworkBlobHeader() {

    }

    public ChatNetworkBlobHeader(AC2Reader data) {
        blobType = data.ReadEnum<ChatNetworkBlobType>();
        blobDispatchType = data.ReadEnum<ChatAsyncMethodId>();
        targetType = data.ReadUInt32();
        targetId = data.ReadUInt32();
        transportType = data.ReadUInt32();
        transportId = data.ReadUInt32();
        cookie = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(blobType);
        data.WriteEnum(blobDispatchType);
        data.Write(targetType);
        data.Write(targetId);
        data.Write(transportType);
        data.Write(transportId);
        data.Write(cookie);
    }
}
