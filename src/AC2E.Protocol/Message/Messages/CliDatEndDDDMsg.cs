using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class CliDatEndDDDMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_DATABASE;

        public MessageOpcode opcode => MessageOpcode.CLIDAT_END_DDD_EVENT;

        public void write(BinaryWriter data) {

        }
    }
}
