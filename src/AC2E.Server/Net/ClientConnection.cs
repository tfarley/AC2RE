using AC2E.Crypto;
using AC2E.Protocol;
using AC2E.Protocol.NetBlob;
using AC2E.Utils.Extensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AC2E.Server.Net {

    internal class ClientConnection {

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

        public void enqueueMessage(INetMessage msg) {
            MemoryStream buffer = new MemoryStream();
            using (BinaryWriter data = new BinaryWriter(buffer)) {
                data.Write((uint)msg.opcode);
                msg.write(data);
            }
            byte[] payload = buffer.ToArray();
            enqueueBlob(msg.blobFlags, msg.queueId, payload);
            Log.Information($"Enqueued msg: {msg} {BitConverter.ToString(payload)}");
        }

        private void enqueueBlob(NetBlobId.Flag blobFlags, NetQueue queueId, byte[] payload) {
            foreach (NetBlobFrag frag in NetBlob.fragmentize(blobFlags, blobSeq, queueId, payload)) {
                fragQueue.Enqueue(frag);
            }
            blobSeq++;
        }
    }
}
