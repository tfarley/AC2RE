namespace AC2E.Def {

    public class QualUpdateLongIntPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateLongInt_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateLongInt_Private
        public LongIntStat type; // _stype
        public long value; // _data

        public QualUpdateLongIntPrivateMsg(AC2Reader data) {
            type = (LongIntStat)data.ReadUInt32();
            value = data.ReadInt64();
        }
    }

    public class QualUpdateLongIntVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateLongInt_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateLongInt_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public LongIntStat type; // _stype
        public long value; // _data

        public QualUpdateLongIntVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (LongIntStat)data.ReadUInt32();
            value = data.ReadInt64();
        }
    }
}
