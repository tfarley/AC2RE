namespace AC2RE.Definitions {

    public class StopFxMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__StopFX_ID;

        // ECM_Physics::RecvEvt_StopFX
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public FxId fxId; // _fx_id

        public StopFxMsg() {

        }

        public StopFxMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            fxId = (FxId)data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(senderIdWithStamp);
            data.Write((uint)fxId);
        }
    }
}
