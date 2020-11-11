namespace AC2E.Def {

    public class PositionMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.EPHEMERAL;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__Position_ID;

        // ECM_Physics::RecvEvt_Position
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public PositionPack pos; // _position_pack

        public PositionMsg() {

        }

        public PositionMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            pos = new(data);
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            pos.write(data);
        }
    }
}
