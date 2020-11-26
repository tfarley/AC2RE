namespace AC2RE.Definitions {

    public class QualUpdateTimestampPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateTimestamp_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateTimestamp_Private
        public TimestampStat type; // _stype
        public double value; // _data

        public QualUpdateTimestampPrivateMsg(TimestampStat type, double value) {
            this.type = type;
            this.value = value;
        }

        public QualUpdateTimestampPrivateMsg(AC2Reader data) {
            type = (TimestampStat)data.ReadUInt32();
            value = data.ReadDouble();
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            data.Write(value);
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

        public QualUpdateTimestampVisualMsg(InstanceIdWithStamp senderIdWithStamp, TimestampStat type, double value) {
            this.senderIdWithStamp = senderIdWithStamp;
            this.type = type;
            this.value = value;
        }

        public QualUpdateTimestampVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (TimestampStat)data.ReadUInt32();
            value = data.ReadDouble();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write((uint)type);
            data.Write(value);
        }
    }
}
