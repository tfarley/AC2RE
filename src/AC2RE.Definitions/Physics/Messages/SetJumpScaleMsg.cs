namespace AC2RE.Definitions {

    public class SetJumpScaleMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__SetJumpScale_ID;
        public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

        // ECM_Physics::RecvEvt_SetJumpScale
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public float jumpScale; // _value

        public SetJumpScaleMsg() {

        }

        public SetJumpScaleMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            jumpScale = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write(jumpScale);
        }
    }
}
