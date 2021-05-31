namespace AC2RE.Definitions {

    public class QualUpdateStringInfoPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateStringInfo_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateStringInfo_Private
        public StringInfoStat type; // _stype
        public StringInfo value; // _data

        public QualUpdateStringInfoPrivateMsg(StringInfoStat type, StringInfo value) {
            this.type = type;
            this.value = value;
        }

        public QualUpdateStringInfoPrivateMsg(AC2Reader data) {
            type = (StringInfoStat)data.ReadUInt32();
            value = new(data);
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            value.write(data);
        }
    }

    public class QualUpdateStringInfoVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateStringInfo_Visual_ID;
        public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

        // ECM_Qualities::RecvEvt_UpdateStringInfo_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public StringInfoStat type; // _stype
        public StringInfo value; // _data

        public QualUpdateStringInfoVisualMsg(InstanceIdWithStamp senderIdWithStamp, StringInfoStat type, StringInfo value) {
            this.senderIdWithStamp = senderIdWithStamp;
            this.type = type;
            this.value = value;
        }

        public QualUpdateStringInfoVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (StringInfoStat)data.ReadUInt32();
            value = new(data);
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write((uint)type);
            value.write(data);
        }
    }
}
