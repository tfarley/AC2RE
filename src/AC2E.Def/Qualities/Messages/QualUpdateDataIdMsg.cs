namespace AC2E.Def {

    public class QualUpdateDataIdPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateDataID_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateDataID_Private
        public uint type; // _stype
        public DataId value; // _data

        public QualUpdateDataIdPrivateMsg(AC2Reader data) {
            type = data.ReadUInt32();
            value = data.ReadDataId();
        }
    }

    public class QualUpdateDataIdVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateDataID_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateDataID_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public DataId value; // _data

        public QualUpdateDataIdVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = data.ReadDataId();
        }
    }
}
