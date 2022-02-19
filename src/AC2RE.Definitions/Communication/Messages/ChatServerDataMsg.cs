using System.IO;

namespace AC2RE.Definitions;

public class ChatServerDataMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
    public NetQueue queueId => NetQueue.LOGON;
    public MessageOpcode opcode => MessageOpcode.Login__ChatServerData;

    // ChatNetworkBlob
    public ChatNetworkBlobHeader header; // m_header
    public IChatAsyncMethod method; // m_payload

    public ChatServerDataMsg() {

    }

    public ChatServerDataMsg(AC2Reader data) {
        uint length = data.ReadUInt32();
        header = new(data);
        uint payloadLength = data.ReadUInt32();
        method = IChatAsyncMethod.read(header.blobDispatchType, header.blobType, data);
    }

    public void write(AC2Writer data) {
        // Placeholder for length
        data.Write((uint)0);
        long headerStart = data.BaseStream.Position;
        header.write(data);
        // Placeholder for length
        data.Write((uint)0);
        long contentStart = data.BaseStream.Position;
        method.write(data);
        long contentEnd = data.BaseStream.Position;

        long length = contentEnd - headerStart;
        data.BaseStream.Seek(headerStart - sizeof(uint), SeekOrigin.Begin);
        data.Write((uint)length);

        long contentLength = contentEnd - contentStart;
        data.BaseStream.Seek(contentStart - sizeof(uint), SeekOrigin.Begin);
        data.Write((uint)contentLength);

        data.BaseStream.Seek(contentEnd, SeekOrigin.Begin);
    }
}
