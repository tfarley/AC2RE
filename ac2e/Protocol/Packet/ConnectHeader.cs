using System.IO;

public class ConnectHeader {

    public double serverTime;
    public ulong connectionAckCookie;
    public ushort recipientId;
    public ushort netId;
    public uint outgoingSeed;
    public uint incomingSeed;

    public void write(BinaryWriter data) {
        data.Write(serverTime);
        data.Write(connectionAckCookie);
        data.Write(recipientId);
        // TODO: Not sure if this should be hardcoded to 0, i.e. netId == recipientId?
        data.Write(netId);
        data.Write(outgoingSeed);
        data.Write(incomingSeed);
        // TODO: Unknown value - padding?
        data.Write((uint)0);
    }
}
