using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class UpdateVisualDescMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__UpdateVisualDesc_ID;

        // ECM_Physics::RecvEvt_UpdateVisualDesc
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public VisualDesc vDesc; // _vdesc

        public UpdateVisualDescMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            vDesc = new VisualDesc(data);
        }
    }
}
