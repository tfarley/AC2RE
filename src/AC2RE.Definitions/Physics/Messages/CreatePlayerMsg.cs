namespace AC2RE.Definitions {

    public class CreatePlayerMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Physics__CreatePlayer;

        // ECM_Physics::RecvEvt_CreatePlayer
        public InstanceId id; // _objectID
        public uint regionId; // _regionID

        public CreatePlayerMsg() {

        }

        public CreatePlayerMsg(AC2Reader data) {
            id = data.ReadInstanceId();
            regionId = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(id);
            data.Write(regionId);
        }
    }
}
