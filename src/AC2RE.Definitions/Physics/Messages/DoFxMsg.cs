namespace AC2RE.Definitions {

    public class DoFxMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Physics__DoFX;
        public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

        // ECM_Physics::RecvEvt_DoFX
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public FxId fxId; // _fx_id
        public float scalar; // __scalar

        public DoFxMsg() {

        }

        public DoFxMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            fxId = (FxId)data.ReadUInt32();
            scalar = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write((uint)fxId);
            data.Write(scalar);
        }
    }
}
