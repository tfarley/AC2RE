using AC2E.Protocol.Packet;
using Serilog;
using System;
using System.IO;
using static AC2E.PacketTool.Reader.NetworkHeaders;

namespace AC2E.PacketTool.Reader {

    public class PcapReader {

        public class PcapHeader {

            public uint magicNumber;
            public ushort versionMajor;
            public ushort versionMinor;
            public uint thisZone;
            public uint sigFigs;
            public uint snapLen;
            public uint network;

            public PcapHeader(BinaryReader data) {
                magicNumber = data.ReadUInt32();
                versionMajor = data.ReadUInt16();
                versionMinor = data.ReadUInt16();
                thisZone = data.ReadUInt32();
                sigFigs = data.ReadUInt32();
                snapLen = data.ReadUInt32();
                network = data.ReadUInt32();
            }
        }

        public class PcapRecordHeader {

            public uint tsSec;
            public uint tsUsec;
            public uint inclLen;
            public uint origLen;

            public PcapRecordHeader(BinaryReader data) {
                tsSec = data.ReadUInt32();
                tsUsec = data.ReadUInt32();
                inclLen = data.ReadUInt32();
                origLen = data.ReadUInt32();
            }
        }

        public class PcapngBlockHeader {

            public uint blockType;
            public uint blockTotalLength;
            public uint interfaceID;
            public uint tsHigh;
            public uint tsLow;
            public uint capturedLen;
            public uint packetLen;

            public PcapngBlockHeader(BinaryReader data) {
                blockType = data.ReadUInt32();
                blockTotalLength = data.ReadUInt32();

                if (blockType == 6) {
                    interfaceID = data.ReadUInt32();
                    tsHigh = data.ReadUInt32();
                    tsLow = data.ReadUInt32();
                    capturedLen = data.ReadUInt32();
                    packetLen = data.ReadUInt32();
                } else if (blockType == 3) {
                    packetLen = data.ReadUInt32();
                    capturedLen = blockTotalLength - 16;
                }
            }
        }

        public PcapHeader pcapHeader { get; private set; }

        public static NetBlobCollection read(Stream input) {
            NetBlobCollection netBlobCollection = new NetBlobCollection();

            using (BinaryReader data = new BinaryReader(input)) {
                uint magicNumber = data.ReadUInt32();
                data.BaseStream.Seek(-4, SeekOrigin.Current);

                if (magicNumber == 0xA1B2C3D4 || magicNumber == 0xD4C3B2A1) {
                    readPcap(data, netBlobCollection);
                } else {
                    readPcapng(data, netBlobCollection);
                }
            }

            return netBlobCollection;
        }

        private static float makeTimestamp(ref ulong epoch, uint high, uint low) {
            ulong time = (((ulong)high << 32) | low);
            if (epoch == 0) {
                epoch = time;
            }
            return (time - epoch) / 10000000000.0f;
        }

        private static void readPcap(BinaryReader data, NetBlobCollection netBlobCollection) {
            PcapHeader pcapHeader = new PcapHeader(data);

            ulong epoch = 0;
            int packetNum = 1;
            while (data.BaseStream.Position < data.BaseStream.Length) {
                PcapRecordHeader recordHeader = new PcapRecordHeader(data);
                float timestamp = makeTimestamp(ref epoch, recordHeader.tsSec, recordHeader.tsUsec);
                byte[] payload = data.ReadBytes((int)recordHeader.inclLen);

                readPacket(payload, packetNum, timestamp, netBlobCollection);

                packetNum++;
            }
        }

        private static void readPcapng(BinaryReader data, NetBlobCollection netBlobCollection) {
            ulong epoch = 0;
            int packetNum = 1;
            while (data.BaseStream.Position < data.BaseStream.Length) {
                PcapngBlockHeader blockHeader = new PcapngBlockHeader(data);
                float timestamp = makeTimestamp(ref epoch, blockHeader.tsHigh, blockHeader.tsLow);
                byte[] payload = data.ReadBytes((int)blockHeader.capturedLen);

                readPacket(payload, packetNum, timestamp, netBlobCollection);

                packetNum++;
            }
        }

        private static void readPacket(byte[] payload, int packetNum, float timestamp, NetBlobCollection netBlobCollection) {
            try {
                using (BinaryReader data = new BinaryReader(new MemoryStream(payload))) {
                    EthernetHeader ethernetHeader = new EthernetHeader(data);
                    IpHeader ipHeader = new IpHeader(data);
                    UdpHeader udpHeader = new UdpHeader(data);

                    NetPacket packet = new NetPacket(data);
                    bool isClientToServer = (udpHeader.dPort >= 9000 && udpHeader.dPort <= 10013);

                    netBlobCollection.addPacket(packet, isClientToServer, packetNum, timestamp);
                }
            } catch (Exception e) {
                Log.Warning(e, $"Failed to read pcap record #{packetNum}");
            }
        }
    }
}
