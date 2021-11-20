namespace AC2RE.Definitions {

    public class SetVelocityScaleMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Physics__SetVelocityScale;
        public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

        // ECM_Physics::RecvEvt_SetVelocityScale
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public float velScale; // _value

        public SetVelocityScaleMsg() {

        }

        public SetVelocityScaleMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            velScale = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write(velScale);
        }
    }
}
