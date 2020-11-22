namespace AC2RE.Definitions {

    public class ReleaseBehaviorMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__ReleaseBehavior_ID;

        // ECM_Physics::RecvEvt_ReleaseBehavior
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public BehaviorId behaviorId; // _behavior_id
        public BehaviorParams behaviorParams; // _params

        public ReleaseBehaviorMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            behaviorId = (BehaviorId)data.ReadUInt32();
            behaviorParams = new(data);
        }
    }
}
