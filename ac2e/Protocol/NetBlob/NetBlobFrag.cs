using System.IO;

public class NetBlobFrag {

    public uint blobSeq;
    public uint blobId;
    public ushort numFrags;
    public ushort fragIndex;
    public NetQueue queueId;
    public byte[] payload;

    public NetBlobFrag() {

    }

    public NetBlobFrag(BinaryReader data) {
        blobSeq = data.ReadUInt32();
        blobId = data.ReadUInt32();
        numFrags = data.ReadUInt16();
        ushort fragSize = data.ReadUInt16();
        fragIndex = data.ReadUInt16();
        queueId = (NetQueue)data.ReadUInt16();

        payload = data.ReadBytes(fragSize - 16);
    }

    public void writeHeader(BinaryWriter data) {
        data.Write(blobSeq);
        data.Write(blobId);
        data.Write(numFrags);
        data.Write((ushort)(payload.Length + 16));
        data.Write(fragIndex);
        data.Write((ushort)queueId);
    }

    public void writePayload(BinaryWriter data) {
        data.Write(payload);
    }
}
