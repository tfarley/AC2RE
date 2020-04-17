using AC2E.Def.Enums;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class CliDatInterrogationResponseMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_DATABASE;

        public MessageOpcode opcode => MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT;

        public Language language;

        public CliDatInterrogationResponseMsg(BinaryReader data) {
            language = (Language)data.ReadUInt32();
            // TODO: Read IterationListData, 88 bytes worth - always?
        }
    }
}
