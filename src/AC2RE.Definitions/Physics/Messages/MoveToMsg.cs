namespace AC2RE.Definitions {

    public class MoveToMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Physics__MoveTo;
        public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

        // ECM_Physics::RecvEvt_MoveTo
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public MovementParameters movementParams; // _params
        public ushort movetoStamp; // _moveto_stamp

        public MoveToMsg() {

        }

        public MoveToMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            movementParams = new(data);
            movetoStamp = data.ReadUInt16();
            data.Align(4);
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            movementParams.write(data);
            data.Write(movetoStamp);
            data.Align(4);
        }
    }
}
