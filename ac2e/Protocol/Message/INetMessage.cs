using System;
using System.IO;

public interface INetMessage {

    public abstract NetBlobId.Flag blobFlags { get; }

    public abstract NetQueue queue { get; }

    public abstract MessageOpcode opcode { get; }

    void write(BinaryWriter data) {
        throw new NotImplementedException();
    }
}
