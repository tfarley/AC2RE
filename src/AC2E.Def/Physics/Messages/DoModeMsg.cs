namespace AC2E.Def {

    public class DoModeMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__DoMode_ID;

        // ECM_Physics::RecvEvt_DoMode
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint modeId; // mode_id

        public DoModeMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            modeId = data.ReadUInt32();
        }
    }
}
