namespace AC2RE.Definitions {

    public class WorldNameMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Admin__WorldName_ID;

        // ECM_Admin::RecvEvt_WorldName
        public StringInfo worldName; // _si

        public WorldNameMsg() {

        }

        public WorldNameMsg(AC2Reader data) {
            worldName = new(data);
        }

        public void write(AC2Writer data) {
            worldName.write(data);
        }
    }
}
