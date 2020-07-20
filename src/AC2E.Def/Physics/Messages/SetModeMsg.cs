namespace AC2E.Def {

    public class SetModeMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__SetMode_ID;

        // ECM_Physics::RecvEvt_SetMode
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public uint modeId; // modeID

        public SetModeMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            modeId = data.ReadUInt32();
        }
    }
}
