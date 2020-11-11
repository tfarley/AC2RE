namespace AC2E.Def {

    public class PositionCellMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__PositionCell_ID;

        // ECM_Physics::RecvEvt_PositionCell
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public PositionPack pos; // __pp

        public PositionCellMsg() {

        }

        public PositionCellMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            pos = new(data);
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            pos.write(data);
        }
    }
}
