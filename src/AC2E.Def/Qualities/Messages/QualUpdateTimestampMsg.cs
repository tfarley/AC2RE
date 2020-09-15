namespace AC2E.Def {

    public class QualUpdateTimestampPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateTimestamp_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateTimestamp_Private
        public TimestampStat type; // _stype
        public double value; // _data

        public QualUpdateTimestampPrivateMsg(AC2Reader data) {
            type = (TimestampStat)data.ReadUInt32();
            value = data.ReadDouble();
        }
    }

    public class QualUpdateTimestampVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateTimestamp_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateTimestamp_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public TimestampStat type; // _stype
        public double value; // _data

        public QualUpdateTimestampVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (TimestampStat)data.ReadUInt32();
            value = data.ReadDouble();
        }
    }
}
