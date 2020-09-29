namespace AC2E.Def {

    public class DeParentMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DeParent_ID;

        // ECM_Physics::RecvEvt_DeParent
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public InstanceIdWithStamp childIdWithPosStamp; // _child_id_and_position_stamp

        public DeParentMsg() {

        }

        public DeParentMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            childIdWithPosStamp = data.ReadInstanceIdWithStamp();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write(childIdWithPosStamp);
        }
    }
}
