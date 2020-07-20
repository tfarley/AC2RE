namespace AC2E.Def {

    public class CLookAtMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.WEENIE;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CLookAt_ID;

        // ECM_Physics::SendEvt_CLookAt
        public InstanceId targetId; // _target_id

        public CLookAtMsg(AC2Reader data) {
            targetId = data.ReadInstanceId();
        }
    }
}
