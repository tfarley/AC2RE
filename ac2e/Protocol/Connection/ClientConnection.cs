using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

public class ClientConnection {

    public readonly ushort id;
    public readonly IPEndPoint endpoint;
    public readonly ulong connectionAckCookie;
    public readonly uint serverSeed;
    public readonly uint clientSeed;

    public bool connected { get; private set; }
    public ISAAC serverIsaac { get; private set; }
    public uint packetSeq;
    public uint highestReceivedPacketSeq;
    public readonly List<uint> nackedSeqs = new List<uint>();
    public uint blobSeq;
    public readonly Queue<NetBlobFrag> fragQueue = new Queue<NetBlobFrag>();
    public float nextAckTime;
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

    public void enqueueMessage(INetMessage message) {
        MemoryStream buffer = new MemoryStream();
        message.write(new BinaryWriter(buffer));
        foreach (NetBlobFrag frag in NetBlob.fragmentize(blobSeq, message.queue, buffer.ToArray())) {
            fragQueue.Enqueue(frag);
        }
        blobSeq++;
    }
}
