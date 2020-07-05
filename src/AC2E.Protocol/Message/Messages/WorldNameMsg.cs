using System.IO;
using System.Text;

namespace AC2E.Protocol {

    public class WorldNameMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.EVENT;
        public MessageOpcode opcode => MessageOpcode.Evt_Admin__WorldName_ID;

        public uint numConnections;
        public uint maxConnections;
        public uint unk1;
        public string worldName;

        public WorldNameMsg() {

        }

        public WorldNameMsg(BinaryReader data) {
            numConnections = data.ReadUInt32();
            maxConnections = data.ReadUInt32();
            unk1 = data.ReadUInt32();
            worldName = data.ReadEncryptedString(Encoding.Unicode);
        }

        public void write(BinaryWriter data) {
            data.Write(numConnections);
            data.Write(maxConnections);
            data.Write(unk1);
            data.WriteEncryptedString(worldName, Encoding.Unicode);
        }
    }
}
