namespace AC2RE.Definitions {

    public class QualUpdateFloatPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateFloat_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateFloat_Private
        public FloatStat type; // _stype
        public float value; // _data

        public QualUpdateFloatPrivateMsg(FloatStat type, float value) {
            this.type = type;
            this.value = value;
        }

        public QualUpdateFloatPrivateMsg(AC2Reader data) {
            type = (FloatStat)data.ReadUInt32();
            value = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            data.Write(value);
        }
    }

    public class QualUpdateFloatVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateFloat_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateFloat_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public FloatStat type; // _stype
        public float value; // _data

        public QualUpdateFloatVisualMsg(InstanceIdWithStamp senderIdWithStamp, FloatStat type, float value) {
            this.senderIdWithStamp = senderIdWithStamp;
            this.type = type;
            this.value = value;
        }

        public QualUpdateFloatVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (FloatStat)data.ReadUInt32();
            value = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write((uint)type);
            data.Write(value);
        }
    }
}
