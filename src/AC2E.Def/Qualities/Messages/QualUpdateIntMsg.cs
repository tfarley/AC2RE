namespace AC2E.Def {

    public class QualUpdateIntPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInt_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateInt_Private
        public uint type; // _stype
        public int value; // _data

        public QualUpdateIntPrivateMsg(AC2Reader data) {
            type = data.ReadUInt32();
            value = data.ReadInt32();
        }
    }

    public class QualUpdateIntVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInt_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateInt_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public int value; // _data

        public QualUpdateIntVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = data.ReadInt32();
        }
    }
}
