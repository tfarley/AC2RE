namespace AC2RE.Definitions;

public class ConnectHeader {

    // CConnectHeader::__unnamed
    public double serverTime; // ServerTime
    public ulong connectionAckCookie; // qwCookie
    public uint netId; // NetID
    public uint outgoingSeed; // OutgoingSeed
    public uint incomingSeed; // IncomingSeed

    public ConnectHeader() {

    }

    public ConnectHeader(AC2Reader data) {
        serverTime = data.ReadDouble();
        connectionAckCookie = data.ReadUInt64();
        netId = data.ReadUInt32();
        outgoingSeed = data.ReadUInt32();
        incomingSeed = data.ReadUInt32();
        // TODO: Unknown value - padding?
        data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.Write(serverTime);
        data.Write(connectionAckCookie);
        data.Write(netId);
        data.Write(outgoingSeed);
        data.Write(incomingSeed);
        // TODO: Unknown value - padding?
        data.Write((uint)0);
    }
}
