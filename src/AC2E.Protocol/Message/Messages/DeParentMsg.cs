using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class DeParentMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DeParent_ID;

        // ECM_Physics::RecvEvt_Contain
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public InstanceIdWithStamp childIdWithPositionStamp; // _child_id_and_position_stamp

        public DeParentMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            childIdWithPositionStamp = data.ReadInstanceIdWithStamp();
        }
    }
}
