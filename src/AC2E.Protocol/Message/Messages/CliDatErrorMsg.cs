using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class CliDatErrorMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
        public NetQueue queueId => NetQueue.DATABASE;
        public MessageOpcode opcode => MessageOpcode.CLIDAT_ERROR_EVENT;

        // CCliDatErrorEvent::CDataFormat
        public QualifiedDataId qdid; // qdid
        public uint error; // dwError

        public CliDatErrorMsg() {

        }

        public CliDatErrorMsg(BinaryReader data) {
            qdid = data.ReadQualifiedDataId();
            error = data.ReadUInt32();
        }

        public void write(BinaryWriter data) {
            data.Write(qdid);
            data.Write(error);
        }
    }
}
