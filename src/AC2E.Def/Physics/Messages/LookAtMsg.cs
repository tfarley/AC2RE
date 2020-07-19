using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class LookAtMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__LookAt_ID;

        // ECM_Physics::RecvEvt_LookAt
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public InstanceId targetId; // _target_id

        public LookAtMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            targetId = data.ReadInstanceId();
        }
    }
}
