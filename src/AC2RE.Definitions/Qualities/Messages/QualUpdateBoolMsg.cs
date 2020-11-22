namespace AC2RE.Definitions {

    public class QualUpdateBoolPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateBool_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateBool_Private
        public BoolStat type; // _stype
        public bool value; // _data

        public QualUpdateBoolPrivateMsg(AC2Reader data) {
            type = (BoolStat)data.ReadUInt32();
            value = data.ReadBoolean();
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

        public QualUpdateBoolVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = (BoolStat)data.ReadUInt32();
            value = data.ReadBoolean();
        }
    }
}
