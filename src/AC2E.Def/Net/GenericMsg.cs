using System.IO;

namespace AC2E.Def {

    public class GenericMsg : INetMessage {

        public NetBlobId.Flag blobFlags { get; set; }
        public NetQueue queueId { get; set; }
        public MessageOpcode opcode { get; set; }

        public byte[] payload;

        public void write(BinaryWriter data) {
            data.Write(payload);
        }
    }
}
