using System.IO;

public class NetBlobFrag {

    public uint blobSeq;
    public uint blobId;
    public ushort fragCount;
    public ushort fragSize;
    public ushort fragIndex;
    public NetQueue queueId;
    public byte[] payload;

    public NetBlobFrag() {

    }

    public NetBlobFrag(BinaryReader data) {
        blobSeq = data.ReadUInt32();
        blobId = data.ReadUInt32();
        fragCount = data.ReadUInt16();
        fragSize = data.ReadUInt16();
        fragIndex = data.ReadUInt16();
        queueId = (NetQueue)data.ReadUInt16();

        payload = data.ReadBytes(fragSize - 16);
    }

    public void writeHeader(BinaryWriter data) {
        fragSize = (ushort)(payload.Length + 16);

        data.Write(blobSeq);
        data.Write(blobId);
        data.Write(fragCount);
        data.Write(fragSize);
        data.Write(fragIndex);
        data.Write((ushort)queueId);
    }

    public void writePayload(BinaryWriter data) {
        data.Write(payload);
    }

    public override string ToString() {
        return $"s {blobSeq} u {blobId} c {fragCount} l {fragSize} i {fragIndex} q {queueId}";
    }
}
