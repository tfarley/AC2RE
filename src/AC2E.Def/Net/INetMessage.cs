using System;

namespace AC2E.Def {

    public interface INetMessage {

        NetBlobId.Flag blobFlags { get; }
        NetQueue queueId { get; }
        MessageOpcode opcode { get; }

        void write(AC2Writer data) {
            throw new NotImplementedException("INetMessage implementor must override write().");
        }
    }
}
