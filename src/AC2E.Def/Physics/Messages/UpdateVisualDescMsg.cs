namespace AC2E.Def {

    public class UpdateVisualDescMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__UpdateVisualDesc_ID;

        // ECM_Physics::RecvEvt_UpdateVisualDesc
        public InstanceIdWithStamp senderIdWithStamp; // sender
        public VisualDesc visualDesc; // _vdesc

        public UpdateVisualDescMsg(AC2Reader data) {
            senderIdWithStamp = data.ReadInstanceIdWithStamp();
            visualDesc = new VisualDesc(data);
        }
    }
}
