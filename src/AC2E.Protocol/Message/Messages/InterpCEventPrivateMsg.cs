using AC2E.Protocol.Event;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class InterpCEventPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;

        public NetQueue queueId => NetQueue.NET_QUEUE_EVENT;

        public MessageOpcode opcode => MessageOpcode.Evt_Interp__InterpCEvent_Private_ID;

        public INetClientEvent netEvent;

        public void write(BinaryWriter data) {
            data.Write((uint)netEvent.funcId);
            // Placeholder for length
            data.Write((uint)0);
            long contentStart = data.BaseStream.Position;
            netEvent.write(data);
            long contentEnd = data.BaseStream.Position;
            long contentLength = contentEnd - contentStart;
            data.BaseStream.Seek(-contentLength - 4, SeekOrigin.Current);
            data.Write((uint)contentLength);
            data.BaseStream.Seek(contentEnd, SeekOrigin.Begin);
        }
    }
}
