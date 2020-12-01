namespace AC2RE.Definitions {

    public class PositionMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__Position_ID;

        // ECM_Physics::RecvEvt_Position
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public PositionPack posPack; // _position_pack

        public PositionMsg() {

        }

        public PositionMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            posPack = new(data);
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            posPack.write(data);
        }
    }
}
