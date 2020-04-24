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

        public static readonly byte ORDERING_TYPE_WEENIE = 0x01;
        public static readonly byte ORDERING_TYPE_UNK1 = 0x02;
        public static readonly byte ORDERING_TYPE_NORMAL = 0x03;

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
            // TODO: Determining order this way doesn't seem correct, see packet with 66:00:01:00 having EVENT queue but WEENIE ordering
            byte orderingType = (queueId == NetQueue.NET_QUEUE_WEENIE || queueId == NetQueue.NET_QUEUE_SECUREWEENIE) ? ORDERING_TYPE_WEENIE : ORDERING_TYPE_NORMAL;
            NetBlob blob = new NetBlob {
                blobId = new NetBlobId(blobFlags, orderingType, 0, blobSeq),
                queueId = queueId,
                payload = payload,
            };
            blob.fragmentize();
            foreach (NetBlobFrag frag in blob.frags.Values) {
                fragQueue.Enqueue(frag);
            }
            blobSeq++;
        }
    }
}
