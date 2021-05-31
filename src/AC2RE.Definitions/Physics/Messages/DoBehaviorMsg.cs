namespace AC2RE.Definitions {

    public class DoBehaviorMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DoBehavior_ID;
        public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

        // ECM_Physics::RecvEvt_DoBehavior
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public BehaviorParams behaviorParams; // _params

        public DoBehaviorMsg() {

        }

        public DoBehaviorMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            behaviorParams = new(data);
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            behaviorParams.write(data);
        }
    }
}
