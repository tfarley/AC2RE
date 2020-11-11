namespace AC2E.Def {

    public class MoveToMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__MoveTo_ID;

        // ECM_Physics::RecvEvt_MoveTo
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public MovementParameters movementParams; // _params
        public ushort movetoStamp; // _moveto_stamp

        public MoveToMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            movementParams = new(data);
            movetoStamp = data.ReadUInt16();
            data.Align(4);
        }
    }
}
