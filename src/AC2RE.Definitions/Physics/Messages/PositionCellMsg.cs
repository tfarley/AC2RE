namespace AC2RE.Definitions {

    public class PositionCellMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Physics__PositionCell;

        // ECM_Physics::RecvEvt_PositionCell
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public PositionPack posPack; // __pp

        public PositionCellMsg() {

        }

        public PositionCellMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            posPack = new(data);
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            posPack.write(data);
        }
    }
}
