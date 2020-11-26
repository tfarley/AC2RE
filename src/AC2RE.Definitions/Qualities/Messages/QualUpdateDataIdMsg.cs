namespace AC2RE.Definitions {

    public class QualUpdateDataIdPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateDataID_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateDataID_Private
        public DataIdStat type; // _stype
        public DataId value; // _data

        public QualUpdateDataIdPrivateMsg(DataIdStat type, DataId value) {
            this.type = type;
            this.value = value;
        }

        public QualUpdateDataIdPrivateMsg(AC2Reader data) {
            type = (DataIdStat)data.ReadUInt32();
            value = data.ReadDataId();
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            data.Write(value);
        }
    }

    public class QualUpdateDataIdVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateDataID_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateDataID_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public DataIdStat type; // _stype
        public DataId value; // _data

        public QualUpdateDataIdVisualMsg(InstanceIdWithStamp senderIdWithStamp, DataIdStat type, DataId value) {
            this.senderIdWithStamp = senderIdWithStamp;
            this.type = type;
            this.value = value;
        }

        public QualUpdateDataIdVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (DataIdStat)data.ReadUInt32();
            value = data.ReadDataId();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write((uint)type);
            data.Write(value);
        }
    }
}
