using AC2E.Def;
using System.IO;

namespace AC2E.Protocol {

    public class WorldNameMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Admin__WorldName_ID;

        // ECM_Admin::RecvEvt_WorldName
        public StringInfo name; // _si

        public WorldNameMsg() {

        }

        public WorldNameMsg(BinaryReader data) {
            name = new StringInfo(data);
        }

        public void write(BinaryWriter data) {
            name.write(data);
        }
    }
}
