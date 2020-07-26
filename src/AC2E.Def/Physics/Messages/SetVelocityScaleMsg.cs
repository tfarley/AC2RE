namespace AC2E.Def {

    public class SetVelocityScaleMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__SetVelocityScale_ID;

        // ECM_Physics::RecvEvt_SetVelocityScale
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public float velScale; // _value

        public SetVelocityScaleMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            velScale = data.ReadSingle();
        }
    }
}
