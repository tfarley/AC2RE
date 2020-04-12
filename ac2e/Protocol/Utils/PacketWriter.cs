using System.IO;

public class PacketWriter {

    public readonly byte[] rawData;
    public readonly BinaryWriter data;

    public PacketWriter(byte[] rawData) {
        this.rawData = rawData;
        data = new BinaryWriter(new MemoryStream(rawData));
    }
}
