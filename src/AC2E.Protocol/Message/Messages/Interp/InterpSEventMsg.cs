using AC2E.WLib;
using System.IO;

namespace AC2E.Protocol {

    public class InterpSEventMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Evt_Interp__InterpSEvent_ID;

        // ECM_Interp::SendEvt_InterpSEvent
        public IServerEvent netEvent;

        public InterpSEventMsg(BinaryReader data) {
            ServerEventFunctionId funcId = (ServerEventFunctionId)data.ReadUInt32();
            uint length = data.ReadUInt32();
            netEvent = IServerEvent.read(funcId, data);
        }
    }
}
