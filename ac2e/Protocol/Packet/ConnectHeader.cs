using System.IO;

public class ConnectHeader {

    public double serverTime;
    public ulong connectionAckCookie;
    public ushort recipientId;
    public ushort netId;
    public uint outgoingSeed;
    public uint incomingSeed;

    public void write(PacketWriter writer, ref uint checksum) {
        BinaryWriter data = writer.data;
        long dataStart = data.BaseStream.Position;

        data.Write(serverTime);
        data.Write(connectionAckCookie);
        data.Write(recipientId);
        // TODO: Not sure if this should be hardcoded to 0, i.e. netId == recipientId?
        data.Write(netId);
        data.Write(outgoingSeed);
        data.Write(incomingSeed);
        // TODO: Unknown value - padding?
        data.Write((uint)0);

        checksum += CryptoUtil.calcChecksum(writer.rawData, dataStart, data.BaseStream.Position - dataStart, true);
    }
}
