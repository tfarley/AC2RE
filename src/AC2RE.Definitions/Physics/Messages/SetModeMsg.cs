namespace AC2RE.Definitions {

    public class SetModeMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Physics__SetMode;
        public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

        // ECM_Physics::RecvEvt_SetMode
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public ModeId modeId; // modeID

        public SetModeMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            modeId = (ModeId)data.ReadUInt32();
        }
    }
}
