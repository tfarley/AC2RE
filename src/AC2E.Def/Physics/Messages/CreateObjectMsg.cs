namespace AC2E.Def {

    public class CreateObjectMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CreateObject_ID;

        // CM_Physics::RecvEvt_CreateObject
        public InstanceId objectId; // objectID
        public VisualDesc vDesc; // _vdesc
        public PhysicsDesc pDesc; // _pdesc
        public WeenieDesc wDesc; // _wdesc

        public CreateObjectMsg() {

        }

        public CreateObjectMsg(AC2Reader data) {
            objectId = data.ReadInstanceId();
            vDesc = new VisualDesc(data);
            pDesc = new PhysicsDesc(data);
            wDesc = new WeenieDesc(data);
        }

        public void write(AC2Writer data) {
            data.Write(objectId);
            vDesc.write(data);
            pDesc.write(data);
            wDesc.write(data);
        }
    }
}
