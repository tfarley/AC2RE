using AC2E.Def;
using AC2E.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AC2E.Server {

    internal class ClientConnection {

        private static readonly float ACK_INTERVAL = 2.0f;
        private static readonly float TIME_SYNC_INTERVAL = 20.0f;

        private static readonly byte ORDERING_TYPE_WEENIE = 0x01;
        private static readonly byte ORDERING_TYPE_UNK1 = 0x02;
        private static readonly byte ORDERING_TYPE_NORMAL = 0x03;

        public readonly ClientId id;
        public readonly IPEndPoint endpoint;
        public readonly Account account;
        public readonly ulong connectionAckCookie;
        public readonly uint outgoingSeed;
        public readonly uint incomingSeed;

        public bool connected { get; private set; }
        public ISAAC? outgoingIsaac { get; private set; }
        public uint packetSeq;
        public uint highestReceivedPacketSeq;
        public uint highestAckedPacketSeq;
        public readonly List<uint> nackedSeqs = new List<uint>();
        public uint blobSeq;
        public readonly Queue<NetBlobFrag> outgoingFragQueue = new Queue<NetBlobFrag>();
        public readonly Dictionary<NetBlobId, NetBlob> incomingBlobs = new Dictionary<NetBlobId, NetBlob>();
        public readonly Queue<NetBlob> incomingCompleteBlobs = new Queue<NetBlob>();
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

        public ClientConnection(ClientId id, IPEndPoint endpoint, Account account) {
            this.id = id;
            this.endpoint = endpoint;
            this.account = account;

            Random rand = new Random();
            connectionAckCookie = rand.NextULong();
            outgoingSeed = rand.NextUInt();
            incomingSeed = rand.NextUInt();
        }

        public void connect() {
            connected = true;

            outgoingIsaac = new ISAAC(outgoingSeed);
            packetSeq = 1;
            highestReceivedPacketSeq = 1;
        }

        public void addFragment(NetBlobFrag frag) {
            // TODO: Need to track processed blob ids and discard if duplicate
            // TODO: Need to handle blob ordering instead of just adding at end of queue
            if (frag.fragCount == 1) {
                incomingCompleteBlobs.Enqueue(new NetBlob(frag));
            } else {
                if (!incomingBlobs.TryGetValue(frag.blobId, out NetBlob? blob)) {
                    blob = new NetBlob(frag);
                    incomingBlobs[frag.blobId] = blob;
                } else {
                    blob.addFragment(frag);
                }

                if (blob.payload != null) {
                    incomingCompleteBlobs.Enqueue(blob);
                    incomingBlobs.Remove(blob.blobId);
                }
            }
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
                outgoingFragQueue.Enqueue(frag);
            }
            blobSeq++;
        }

        public void flushSend(NetInterface netInterface, double time, float elapsedTime) {
            while (connected && (echoRequestedLocalTime != -1.0f || elapsedTime > nextAckTime || elapsedTime > nextTimeSyncTime || outgoingFragQueue.Count > 0 || nacksToResend.Count > 0)) {
                if (nacksToResend.TryPeek(out SendablePacket packet) && packet.netInterface == netInterface) {
                    rawSendPacket(packet);
                    nacksToResend.Dequeue();
                } else {
                    sendPacket(netInterface, time, elapsedTime, new NetPacket());
                }
            }
        }

        public bool sendPacket(NetInterface netInterface, double time, float elapsedTime, NetPacket packet) {
            packet.recipientId = id.id;
            packet.interval = (ushort)elapsedTime;
            // TODO: Need to advance this?
            packet.iteration = 1;

            if (connected) {
                packet.flags |= NetPacket.Flag.ENCRYPTED_CHECKSUM;

                int remainingSize = NetPacket.MAX_SIZE - 20; // Subtracts packet header

                // Fill with fragments until full
                if (outgoingFragQueue.Count > 0) {
                    packet.flags |= NetPacket.Flag.FRAGMENTS;

                    while (outgoingFragQueue.TryPeek(out NetBlobFrag? frag) && remainingSize >= frag.fragSize) {
                        packet.frags.Add(frag);
                        outgoingFragQueue.Dequeue();
                        remainingSize -= frag.fragSize;
                    }
                }

                // Allocate remaining space to optional headers
                if (remainingSize > 4 && elapsedTime > nextAckTime) {
                    packet.ackHeader = highestReceivedPacketSeq;
                    nextAckTime = elapsedTime + ACK_INTERVAL;
                    remainingSize -= 4;
                }

                if (remainingSize > 8 && elapsedTime > nextTimeSyncTime) {
                    packet.timeSyncHeader = time;
                    nextTimeSyncTime = elapsedTime + TIME_SYNC_INTERVAL;
                    remainingSize -= 8;
                }

                if (remainingSize > 8 && echoRequestedLocalTime != -1.0f) {
                    packet.echoResponseHeader = new EchoResponseHeader {
                        localTime = echoRequestedLocalTime,
                        holdingTime = echoRequestedServerTime - elapsedTime,
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

            sentSeqToPackets.GetOrCreate(packet.seq).Add(sendablePacket);

            return rawSendPacket(sendablePacket);
        }

        private bool rawSendPacket(SendablePacket sendablePacket) {
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
                        packet.isaacXor = outgoingIsaac!.next();
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

                Log.Debug($"SENT [{packetLength}] to {endpoint} | {packet}\n{BitConverter.ToString(sendBuffer, 0, packetLength)}.");

                return sendablePacket.netInterface.sendTo(sendBuffer, packetLength, endpoint); ;
            }
        }

        public void ackPacket(uint seq) {
            for (uint i = highestAckedPacketSeq + 1; i <= seq; i++) {
                sentSeqToPackets.Remove(i);
            }
            highestAckedPacketSeq = seq;
        }

        public void nackPacket(uint seq) {
            if (sentSeqToPackets.TryGetValue(seq, out List<SendablePacket>? packets)) {
                foreach (SendablePacket packet in packets) {
                    packet.packet.flags |= NetPacket.Flag.RETRANSMITTING;
                    nacksToResend.Enqueue(packet);
                }
            }
        }
    }
}
