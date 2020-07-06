using AC2E.Def;
using AC2E.WLib;
using System.IO;

namespace AC2E.Protocol {

    public class InterpCEventVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Interp__InterpCEvent_Visual_ID;

        // ECM_Interp::RecvEvt_InterpCEvent_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public IClientEvent netEvent;

        public InterpCEventVisualMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            ClientEventFunctionId funcId = (ClientEventFunctionId)data.ReadUInt32();
            uint length = data.ReadUInt32();
            netEvent = IClientEvent.read(funcId, data);
        }
    }
}
