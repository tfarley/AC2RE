using System.IO;

public class EchoResponseHeader {

    public float localTime;
    public float localToServerTimeDelta;

    public void write(PacketWriter writer, ref uint checksum) {
        BinaryWriter data = writer.data;
        long dataStart = data.BaseStream.Position;

        data.Write(localTime);
        data.Write(localToServerTimeDelta);

        checksum += CryptoUtil.calcChecksum(writer.rawData, dataStart, data.BaseStream.Position - dataStart, true);
    }
}
