using AC2E.Def.Extensions;
using AC2E.Protocol.NetBlob;
using System.IO;
using System.Text;

namespace AC2E.Protocol.Message.Messages {

    public class WorldNameMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_EVENT;

        public MessageOpcode opcode => MessageOpcode.Evt_Admin__WorldName_ID;

        public uint numConnections;
        public uint maxConnections;
        public uint unk1;
        public string worldName;
        public uint unk2;
        public uint unk3;
        public ushort unk4;

        public WorldNameMsg() {

        }

        public WorldNameMsg(BinaryReader data) {
            numConnections = data.ReadUInt32();
            maxConnections = data.ReadUInt32();
            unk1 = data.ReadUInt32();
            worldName = data.ReadEncryptedString(Encoding.Unicode);
            unk2 = data.ReadUInt32();
            unk3 = data.ReadUInt32();
            unk4 = data.ReadUInt16();
        }

        public void write(BinaryWriter data) {
            data.Write(numConnections);
            data.Write(maxConnections);
            data.Write(unk1);
            data.WriteEncryptedString(worldName, Encoding.Unicode);
            data.Write(unk2);
            data.Write(unk3);
            data.Write(unk4);
        }
    }
}
