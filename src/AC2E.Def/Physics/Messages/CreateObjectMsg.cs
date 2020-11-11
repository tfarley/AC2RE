namespace AC2E.Def {

    public class CreateObjectMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Physics__CreateObject_ID;

        // CM_Physics::RecvEvt_CreateObject
        public InstanceId id; // objectID
        public VisualDesc visualDesc; // _vdesc
        public PhysicsDesc physicsDesc; // _pdesc
        public WeenieDesc weenieDesc; // _wdesc

        public CreateObjectMsg() {

        }

        public CreateObjectMsg(AC2Reader data) {
            id = data.ReadInstanceId();
            visualDesc = new(data);
            physicsDesc = new(data);
            weenieDesc = new(data);
        }

        public void write(AC2Writer data) {
            data.Write(id);
            visualDesc.write(data);
            physicsDesc.write(data);
            weenieDesc.write(data);
        }
    }
}
