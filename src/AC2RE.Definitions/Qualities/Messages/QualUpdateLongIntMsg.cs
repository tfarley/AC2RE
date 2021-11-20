namespace AC2RE.Definitions {

    public class QualUpdateLongIntPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Qualities__UpdateLongInt_Private;

        // ECM_Qualities::RecvEvt_UpdateLongInt_Private
        public LongIntStat type; // _stype
        public long value; // _data

        public QualUpdateLongIntPrivateMsg(LongIntStat type, long value) {
            this.type = type;
            this.value = value;
        }

        public QualUpdateLongIntPrivateMsg(AC2Reader data) {
            type = (LongIntStat)data.ReadUInt32();
            value = data.ReadInt64();
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            data.Write(value);
        }
    }

    public class QualUpdateLongIntVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Qualities__UpdateLongInt_Visual;
        public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

        // ECM_Qualities::RecvEvt_UpdateLongInt_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public LongIntStat type; // _stype
        public long value; // _data

        public QualUpdateLongIntVisualMsg(InstanceIdWithStamp senderIdWithStamp, LongIntStat type, long value) {
            this.senderIdWithStamp = senderIdWithStamp;
            this.type = type;
            this.value = value;
        }

        public QualUpdateLongIntVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (LongIntStat)data.ReadUInt32();
            value = data.ReadInt64();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write((uint)type);
            data.Write(value);
        }
    }
}
