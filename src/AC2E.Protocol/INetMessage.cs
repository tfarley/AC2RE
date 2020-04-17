using AC2E.Protocol.NetBlob;
using System;
using System.IO;

namespace AC2E.Protocol {

    public interface INetMessage {

        public abstract NetBlobId.Flag blobFlags { get; }

        public abstract NetQueue queueId { get; }

        public abstract MessageOpcode opcode { get; }

        void write(BinaryWriter data) {
            throw new NotImplementedException();
        }
    }
}
