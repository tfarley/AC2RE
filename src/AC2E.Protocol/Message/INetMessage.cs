using AC2E.Protocol.NetBlob;
using System;
using System.IO;

namespace AC2E.Protocol.Message {

    public interface INetMessage {

        NetBlobId.Flag blobFlags { get; }

        NetQueue queueId { get; }

        MessageOpcode opcode { get; }

        void write(BinaryWriter data) {
            throw new NotImplementedException();
        }
    }
}
