namespace AC2E.Def {

    public class QualUpdatePositionPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdatePosition_Private_ID;

        // ECM_Qualities::RecvEvt_UpdatePosition_Private
        public PositionStat type; // _stype
        public Position value; // _data

        public QualUpdatePositionPrivateMsg(AC2Reader data) {
            type = (PositionStat)data.ReadUInt32();
            value = new Position(data);
        }
    }

    public class QualUpdatePositionVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdatePosition_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdatePosition_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public PositionStat type; // _stype
        public Position value; // _data

        public QualUpdatePositionVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (PositionStat)data.ReadUInt32();
            value = new Position(data);
        }
    }
}
