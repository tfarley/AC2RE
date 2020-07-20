namespace AC2E.Def {

    public class QualUpdateStringInfoPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateStringInfo_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateStringInfo_Private
        public uint type; // _stype
        public StringInfo value; // _data

        public QualUpdateStringInfoPrivateMsg(AC2Reader data) {
            type = data.ReadUInt32();
            value = new StringInfo(data);
        }
    }

    public class QualUpdateStringInfoVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateStringInfo_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateStringInfo_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public StringInfo value; // _data

        public QualUpdateStringInfoVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = new StringInfo(data);
        }
    }
}
