namespace AC2E.Def {

    public class QualUpdateInstanceIdPrivateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInstanceID_Private_ID;

        // ECM_Qualities::RecvEvt_UpdateInstanceID_Private
        public uint type; // _stype
        public InstanceId value; // _data

        public QualUpdateInstanceIdPrivateMsg(AC2Reader data) {
            type = data.ReadUInt32();
            value = data.ReadInstanceId();
        }
    }

    public class QualUpdateInstanceIdVisualMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Qualities__UpdateInstanceID_Visual_ID;

        // ECM_Qualities::RecvEvt_UpdateInstanceID_Visual
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint type; // _stype
        public InstanceId value; // _data

        public QualUpdateInstanceIdVisualMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            type = data.ReadUInt32();
            value = data.ReadInstanceId();
        }
    }
}
