using System;
using System.Net;

public class ClientConnection {

    public readonly ushort id;
    public readonly IPEndPoint endpoint;
    public readonly ulong connectionAckCookie;
    public readonly uint serverSeed;
    public readonly uint clientSeed;

    public bool connected { get; private set; }
    public ISAAC serverIsaac { get; private set; }
    public uint seqId;
    public float echoRequestedLocalTime = -1.0f;

    public ClientConnection(ushort id, IPEndPoint endpoint) {
        this.id = id;
        this.endpoint = endpoint;

        Random rand = new Random();
        connectionAckCookie = rand.NextULong();
        serverSeed = rand.NextUInt();
        clientSeed = rand.NextUInt();
    }

    public void connect() {
        connected = true;

        serverIsaac = new ISAAC(serverSeed);
    }
}
