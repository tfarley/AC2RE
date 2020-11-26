namespace AC2RE.Definitions {

    public class QualUpdateBoolPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateBool_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateBool_Private
        public BoolStat type; // _stype
        public bool value; // _data

        public QualUpdateBoolPrivateMsg(BoolStat type, bool value) {
            this.type = type;
            this.value = value;
        }

        public QualUpdateBoolPrivateMsg(AC2Reader data) {
            type = (BoolStat)data.ReadUInt32();
            value = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            data.Write(value);
        }
    }

    public class QualUpdateBoolVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateBool_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateBool_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public BoolStat type; // _stype
        public bool value; // _data

        public QualUpdateBoolVisualMsg(InstanceIdWithStamp senderIdWithStamp, BoolStat type, bool value) {
            this.senderIdWithStamp = senderIdWithStamp;
            this.type = type;
            this.value = value;
        }

        public QualUpdateBoolVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (BoolStat)data.ReadUInt32();
            value = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write((uint)type);
            data.Write(value);
        }
    }
}
