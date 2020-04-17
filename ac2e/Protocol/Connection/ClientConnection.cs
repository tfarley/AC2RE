using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

public class ClientConnection {

    public readonly ushort id;
    public readonly IPEndPoint endpoint;
    public readonly string accountName;
    public readonly ulong connectionAckCookie;
    public readonly uint serverSeed;
    public readonly uint clientSeed;

    public bool connected { get; private set; }
    public ISAAC serverIsaac { get; private set; }
    public uint packetSeq;
    public uint highestReceivedPacketSeq;
    public uint highestAckedPacketSeq;
    public readonly List<uint> nackedSeqs = new List<uint>();
    public uint blobSeq;
    public readonly Queue<NetBlobFrag> fragQueue = new Queue<NetBlobFrag>();
    public float nextAckTime;
    public float nextTimeSyncTime;
    public float echoRequestedLocalTime = -1.0f;

    public ClientConnection(ushort id, IPEndPoint endpoint, string accountName) {
        this.id = id;
        this.endpoint = endpoint;
        this.accountName = accountName;

        Random rand = new Random();
        connectionAckCookie = rand.NextULong();
        serverSeed = rand.NextUInt();
        clientSeed = rand.NextUInt();
    }

    public void connect() {
        connected = true;

        serverIsaac = new ISAAC(serverSeed);
        packetSeq = 2;
        highestReceivedPacketSeq = 1;
    }

    public void enqueueMessage(INetMessage message) {
        MemoryStream buffer = new MemoryStream();
        using (BinaryWriter data = new BinaryWriter(buffer)) {
            data.Write((uint)message.opcode);
            message.write(data);
        }
        byte[] dataArray = buffer.ToArray();
        ALog.info($"Enqueued msg: {message} {BitConverter.ToString(dataArray)}");
        foreach (NetBlobFrag frag in NetBlob.fragmentize(message.blobFlags, blobSeq, message.queue, dataArray)) {
            fragQueue.Enqueue(frag);
        }
        blobSeq++;
    }
}
