using System.IO;

public class Fragment {

    public ulong blobId;
    public ushort numFrags;
    public ushort blobFragSize;
    public ushort blobNum;
    public ushort queueId;
    public byte[] payload;

    public Fragment(BinaryReader data) {
        blobId = data.ReadUInt64();
        numFrags = data.ReadUInt16();
        blobFragSize = data.ReadUInt16();
        blobNum = data.ReadUInt16();
        queueId = data.ReadUInt16();

        payload = data.ReadBytes(blobFragSize - 16);
    }

    public void write(PacketWriter writer, ref uint checksum) {
        BinaryWriter data = writer.data;
        long dataStart = data.BaseStream.Position;

        data.Write(blobId);
        data.Write(numFrags);
        data.Write(blobFragSize);
        data.Write(blobNum);
        data.Write(queueId);

        checksum += CryptoUtil.calcChecksum(writer.rawData, dataStart, data.BaseStream.Position - dataStart, true);

        dataStart = data.BaseStream.Position;

        data.Write(payload);

        checksum += CryptoUtil.calcChecksum(writer.rawData, dataStart, data.BaseStream.Position - dataStart, true);
    }
}
