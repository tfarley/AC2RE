using AC2E.Dat;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class CliDatRequestDataMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_DATABASE;

        public MessageOpcode opcode => MessageOpcode.CLIDAT_REQUEST_DATA_EVENT;

        public DbType fileDbType;
        public uint fileId;

        public CliDatRequestDataMsg(BinaryReader data) {
            fileDbType = (DbType)data.ReadUInt32();
            fileId = data.ReadUInt32();
        }
    }
}
