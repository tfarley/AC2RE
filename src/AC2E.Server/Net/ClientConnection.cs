using AC2E.Def;
using AC2E.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AC2E.Server {

    internal class ClientConnection {

        private static readonly float ACK_INTERVAL = 2.0f;
        private static readonly float TIME_SYNC_INTERVAL = 20.0f;

        private static readonly byte ORDERING_TYPE_WEENIE = 0x01;
        private static readonly byte ORDERING_TYPE_UNK1 = 0x02;
        private static readonly byte ORDERING_TYPE_NORMAL = 0x03;

        public readonly ushort id;
        public readonly IPEndPoint endpoint;
        public readonly string accountName;
        public readonly ulong connectionAckCookie;
        public readonly uint outgoingSeed;
        public readonly uint incomingSeed;

        public bool connected { get; private set; }
        public ISAAC outgoingIsaac { get; private set; }
        public uint packetSeq;
        public uint highestReceivedPacketSeq;
        public uint highestAckedPacketSeq;
        public readonly List<uint> nackedSeqs = new List<uint>();
        public uint blobSeq;
        public readonly Queue<NetBlobFrag> fragQueue = new Queue<NetBlobFrag>();
        public float nextAckTime;
        public float nextTimeSyncTime;
        public float echoRequestedLocalTime = -1.0f;
        public float echoRequestedServerTime;

        private readonly byte[] sendBuffer = new byte[NetPacket.MAX_SIZE];

        private readonly Dictionary<uint, List<SendablePacket>> sentSeqToPackets = new Dictionary<uint, List<SendablePacket>>();
        private readonly Queue<SendablePacket> nacksToResend = new Queue<SendablePacket>();

        private struct SendablePacket {

            public NetPacket packet;
            public NetInterface netInterface;
        }

        public ClientConnection(ushort id, IPEndPoint endpoint, string accountName) {
            this.id = id;
            this.endpoint = endpoint;
            this.accountName = accountName;

            Random rand = new Random();
            connectionAckCookie = rand.NextULong();
            outgoingSeed = rand.NextUInt();
            incomingSeed = rand.NextUInt();
        }

        public void connect(float serverTime) {
            connected = true;

            outgoingIsaac = new ISAAC(outgoingSeed);
            packetSeq = 1;
            highestReceivedPacketSeq = 1;

            nextAckTime = serverTime + ACK_INTERVAL;
            nextTimeSyncTime = serverTime + TIME_SYNC_INTERVAL;
        }

        public void enqueueMessage(INetMessage msg) {
            MemoryStream buffer = new MemoryStream();
            using (AC2Writer data = new AC2Writer(buffer)) {
                data.Write((uint)msg.opcode);
                msg.write(data);
            }
            byte[] payload = buffer.ToArray();
            enqueueBlob(msg.blobFlags, msg.queueId, payload);
            Log.Information($"Enqueued msg: {msg} {BitConverter.ToString(payload)}");
        }

        private void enqueueBlob(NetBlobId.Flag blobFlags, NetQueue queueId, byte[] payload) {
            // TODO: Determining order this way doesn't seem correct, see packet with 66:00:01:00 having EVENT queue but WEENIE ordering
            byte orderingType = (queueId == NetQueue.WEENIE || queueId == NetQueue.SECUREWEENIE) ? ORDERING_TYPE_WEENIE : ORDERING_TYPE_NORMAL;
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

        public async Task flushSendAsync(NetInterface netInterface, float serverTime) {
            while (connected && (echoRequestedLocalTime != -1.0f || serverTime > nextAckTime || serverTime > nextTimeSyncTime || fragQueue.Count > 0 || nacksToResend.Count > 0)) {
                if (nacksToResend.TryPeek(out SendablePacket packet) && packet.netInterface == netInterface) {
                    await rawSendPacketAsync(packet);
                    nacksToResend.Dequeue();
                } else {
                    await sendPacketAsync(netInterface, serverTime, new NetPacket());
                }
            }
        }

        public async Task<bool> sendPacketAsync(NetInterface netInterface, float serverTime, NetPacket packet) {
            packet.recipientId = id;
            packet.interval = (ushort)serverTime;
            // TODO: Need to advance this?
            packet.iteration = 1;

            if (connected) {
                packet.flags |= NetPacket.Flag.ENCRYPTED_CHECKSUM;

                int remainingSize = NetPacket.MAX_SIZE - 20; // Subtracts packet header

                // Fill with fragments until full
                if (fragQueue.Count > 0) {
                    packet.flags |= NetPacket.Flag.FRAGMENTS;

                    while (fragQueue.TryPeek(out NetBlobFrag frag) && remainingSize >= frag.fragSize) {
                        packet.frags.Add(frag);
                        fragQueue.Dequeue();
                        remainingSize -= frag.fragSize;
                    }
                }

                // Allocate remaining space to optional headers
                if (remainingSize > 4 && serverTime > nextAckTime) {
                    packet.ackHeader = highestReceivedPacketSeq;
                    nextAckTime = serverTime + ACK_INTERVAL;
                    remainingSize -= 4;
                }

                if (remainingSize > 8 && serverTime > nextTimeSyncTime) {
                    packet.timeSyncHeader = serverTime;
                    nextTimeSyncTime = serverTime + TIME_SYNC_INTERVAL;
                    remainingSize -= 8;
                }

                if (remainingSize > 8 && echoRequestedLocalTime != -1.0f) {
                    packet.echoResponseHeader = new EchoResponseHeader {
                        localTime = echoRequestedLocalTime,
                        holdingTime = echoRequestedServerTime - serverTime,
                    };
                    echoRequestedLocalTime = -1.0f;
                    remainingSize -= 8;
                }

                // If purely ACK or NACKS, don't increment sequence number and don't encrypt checksum
                if (packet.flags != (NetPacket.Flag.PAK | NetPacket.Flag.ENCRYPTED_CHECKSUM) && packet.flags != (NetPacket.Flag.NAK | NetPacket.Flag.ENCRYPTED_CHECKSUM)) {
                    packetSeq++;
                } else {
                    packet.flags &= ~NetPacket.Flag.ENCRYPTED_CHECKSUM;
                }
            }

            packet.seq = packetSeq;

            SendablePacket sendablePacket = new SendablePacket {
                packet = packet,
                netInterface = netInterface,
            };

            bool sendResult = await rawSendPacketAsync(sendablePacket);

            sentSeqToPackets.GetOrCreate(packet.seq).Add(sendablePacket);

            return sendResult;
        }

        private async Task<bool> rawSendPacketAsync(SendablePacket sendablePacket) {
            NetPacket packet = sendablePacket.packet;
            using (AC2Writer data = new AC2Writer(new MemoryStream(sendBuffer))) {
                // Write header
                packet.writeHeader(data);
                uint headerChecksum = AC2Crypto.calcChecksum(sendBuffer, 0, data.BaseStream.Position, true);

                // Write optional headers
                long contentStart = data.BaseStream.Position;
                uint contentChecksum = 0;
                packet.writeOptionalHeaders(data, sendBuffer, ref contentChecksum);

                // Write blob fragments
                if (packet.flags.HasFlag(NetPacket.Flag.FRAGMENTS)) {
                    foreach (NetBlobFrag frag in packet.frags) {
                        long dataStart = data.BaseStream.Position;
                        frag.writeHeader(data);
                        contentChecksum += AC2Crypto.calcChecksum(sendBuffer, dataStart, data.BaseStream.Position - dataStart, true);

                        dataStart = data.BaseStream.Position;
                        frag.writePayload(data);
                        contentChecksum += AC2Crypto.calcChecksum(sendBuffer, dataStart, data.BaseStream.Position - dataStart, true);
                    }
                }

                // Encrypt checksum if necessary
                if (packet.flags.HasFlag(NetPacket.Flag.ENCRYPTED_CHECKSUM)) {
                    if (!packet.hasIsaacXor) {
                        packet.isaacXor = outgoingIsaac.next();
                        packet.hasIsaacXor = true;
                    }
                    contentChecksum ^= packet.isaacXor;
                }

                int packetLength = (int)data.BaseStream.Position;
                ushort contentLength = (ushort)(packetLength - contentStart);

                // Replace the content length and also update the checksum
                BitConverter.GetBytes(contentLength).CopyTo(sendBuffer, 16);
                headerChecksum += contentLength;

                // Replace the checksum
                BitConverter.GetBytes(headerChecksum + contentChecksum).CopyTo(sendBuffer, 8);

                Log.Debug($"Send[{packetLength}] to {endpoint} - {BitConverter.ToString(sendBuffer, 0, packetLength)}.");

                bool sendResult = await sendablePacket.netInterface.sendToAsync(sendBuffer, packetLength, endpoint);

                Log.Debug($"SENT: {packet}");

                return sendResult;
            }
        }

        public void ackPacket(uint seq) {
            for (uint i = highestAckedPacketSeq + 1; i <= seq; i++) {
                sentSeqToPackets.Remove(i);
            }
            highestAckedPacketSeq = seq;
        }

        public void nackPacket(uint seq) {
            if (sentSeqToPackets.TryGetValue(seq, out List<SendablePacket> packets)) {
                foreach (SendablePacket packet in packets) {
                    packet.packet.flags |= NetPacket.Flag.RETRANSMITTING;
                    nacksToResend.Enqueue(packet);
                }
            }
        }
    }
}
