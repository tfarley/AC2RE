namespace AC2RE.Definitions {

    public class StopBehaviorMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__StopBehavior_ID;

        // ECM_Physics::RecvEvt_StopBehavior
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public BehaviorId behaviorId; // bvrID

        public StopBehaviorMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            behaviorId = (BehaviorId)data.ReadUInt32();
        }
    }
}
