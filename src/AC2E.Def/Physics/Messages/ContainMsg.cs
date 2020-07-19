using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class ContainMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__Contain_ID;

        // ECM_Physics::RecvEvt_Contain
        public InstanceIdWithStamp childIdWithPositionStamp; // _child_id_and_position_stamp

        public ContainMsg(BinaryReader data) {
            childIdWithPositionStamp = data.ReadInstanceIdWithStamp();
        }
    }
}
