using AC2E.Def;
using System.IO;

namespace AC2E.Def {

    public class ReleaseBehaviorMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__ReleaseBehavior_ID;

        // ECM_Physics::RecvEvt_ReleaseBehavior
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint behaviorId; // _behavior_id
        public BehaviorParams behaviorParams; // _params

        public ReleaseBehaviorMsg(BinaryReader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            behaviorId = data.ReadUInt32();
            behaviorParams = new BehaviorParams(data);
        }
    }
}
