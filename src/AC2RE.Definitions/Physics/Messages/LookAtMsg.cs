namespace AC2RE.Definitions {

    public class LookAtMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__LookAt_ID;

        // ECM_Physics::RecvEvt_LookAt
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public InstanceId targetId; // _target_id

        public LookAtMsg() {

        }

        public LookAtMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            targetId = data.ReadInstanceId();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write(targetId);
        }
    }
}
