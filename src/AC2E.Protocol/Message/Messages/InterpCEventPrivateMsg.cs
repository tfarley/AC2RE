using AC2E.WLib;
using System.IO;

namespace AC2E.Protocol {

    public class InterpCEventPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Interp__InterpCEvent_Private_ID;

        // ECM_Interp::RecvEvt_InterpCEvent_Private
        public IClientEvent netEvent;

        public InterpCEventPrivateMsg() {

        }

        public InterpCEventPrivateMsg(BinaryReader data) {
            ClientEventFunctionId funcId = (ClientEventFunctionId)data.ReadUInt32();
            uint length = data.ReadUInt32();
            netEvent = IClientEvent.read(funcId, data);
        }

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
