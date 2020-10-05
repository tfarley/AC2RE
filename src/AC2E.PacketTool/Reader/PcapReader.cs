using AC2E.Def;
using Serilog;
using System;
using System.IO;

namespace AC2E.PacketTool {

    public static class PcapReader {

        public static NetBlobCollection read(Stream input) {
            NetBlobCollection netBlobCollection = new NetBlobCollection();

            using (AC2Reader data = new AC2Reader(input)) {
                uint magicNumber = data.ReadUInt32();
                data.BaseStream.Seek(-4, SeekOrigin.Current);

                if (magicNumber == PcapHeader.MAGIC_NUMBER_1 || magicNumber == PcapHeader.MAGIC_NUMBER_2) {
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

        private static void readPcap(AC2Reader data, NetBlobCollection netBlobCollection) {
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

        private static void readPcapng(AC2Reader data, NetBlobCollection netBlobCollection) {
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
                using (AC2Reader data = new AC2Reader(new MemoryStream(payload))) {
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
