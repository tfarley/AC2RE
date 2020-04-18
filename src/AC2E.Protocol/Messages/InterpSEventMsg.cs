using AC2E.Interp;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class InterpSEventMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;

        public NetQueue queueId => NetQueue.NET_QUEUE_WEENIE;

        public MessageOpcode opcode => MessageOpcode.Evt_Interp__InterpSEvent_ID;

        public FunctionId funcId;
        public byte[] payload;

        public InterpSEventMsg(BinaryReader data) {
            funcId = data.ReadUInt32();
            uint payloadLen = data.ReadUInt32();
            payload = data.ReadBytes((int)payloadLen);
        }
    }
}
