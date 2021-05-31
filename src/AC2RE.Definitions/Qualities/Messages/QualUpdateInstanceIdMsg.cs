namespace AC2RE.Definitions {

    public class QualUpdateInstanceIdPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInstanceID_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateInstanceID_Private
        public InstanceIdStat type; // _stype
        public InstanceId value; // _data

        public QualUpdateInstanceIdPrivateMsg(InstanceIdStat type, InstanceId value) {
            this.type = type;
            this.value = value;
        }

        public QualUpdateInstanceIdPrivateMsg(AC2Reader data) {
            type = (InstanceIdStat)data.ReadUInt32();
            value = data.ReadInstanceId();
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            data.Write(value);
        }
    }

    public class QualUpdateInstanceIdVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInstanceID_Visual_ID;
        public OrderingType orderingType => OrderingType.VISUAL_ORDERED;

        // ECM_Qualities::RecvEvt_UpdateInstanceID_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public InstanceIdStat type; // _stype
        public InstanceId value; // _data

        public QualUpdateInstanceIdVisualMsg(InstanceIdWithStamp senderIdWithStamp, InstanceIdStat type, InstanceId value) {
            this.senderIdWithStamp = senderIdWithStamp;
            this.type = type;
            this.value = value;
        }

        public QualUpdateInstanceIdVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (InstanceIdStat)data.ReadUInt32();
            value = data.ReadInstanceId();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write((uint)type);
            data.Write(value);
        }
    }
}
