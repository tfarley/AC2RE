using AC2RE.Definitions;
using System;
using System.IO;

namespace AC2RE.PacketTool;

internal static class PcapReader {

    public static NetBlobCollection read(Stream input) {
        NetBlobCollection netBlobCollection = new();

        using (AC2Reader data = new(input)) {
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

    private static double makeTimestampPcap(ref double epoch, uint seconds, uint microseconds) {
        double time = seconds + (microseconds * 0.000001);
        if (epoch == 0.0) {
            epoch = time;
        }
        return time - epoch;
    }

    private static void readPcap(AC2Reader data, NetBlobCollection netBlobCollection) {
        PcapHeader pcapHeader = new(data);

        double epoch = 0.0;
        int packetNum = 1;
        while (data.BaseStream.Position < data.BaseStream.Length) {
            PcapRecordHeader recordHeader = new(data);
            double timestamp = makeTimestampPcap(ref epoch, recordHeader.tsSec, recordHeader.tsUsec);
            byte[] payload = data.ReadBytes((int)recordHeader.inclLen);

            readPacket(payload, packetNum, timestamp, netBlobCollection);

            packetNum++;
        }
    }

    private static double makeTimestampPcapng(ref ulong epoch, uint high, uint low) {
        ulong time = (((ulong)high << 32) | low);
        if (epoch == 0) {
            epoch = time;
        }
        return (time - epoch) / 1000000.0;
    }

    private static void readPcapng(AC2Reader data, NetBlobCollection netBlobCollection) {
        ulong epoch = 0;
        int packetNum = 1;
        while (data.BaseStream.Position < data.BaseStream.Length) {
            PcapngBlockHeader blockHeader = new(data);
            double timestamp = makeTimestampPcapng(ref epoch, blockHeader.tsHigh, blockHeader.tsLow);
            byte[] payload = data.ReadBytes((int)blockHeader.capturedLen);

            readPacket(payload, packetNum, timestamp, netBlobCollection);

            packetNum++;
        }
    }

    private static void readPacket(byte[] payload, int packetNum, double timestamp, NetBlobCollection netBlobCollection) {
        try {
            using (AC2Reader data = new(new MemoryStream(payload))) {
                EthernetHeader ethernetHeader = new(data);
                IpHeader ipHeader = new(data);
                UdpHeader udpHeader = new(data);

                NetPacket packet = new(data);
                bool isClientToServer = (udpHeader.dPort >= 9000 && udpHeader.dPort <= 10013);

                netBlobCollection.addPacket(packet, isClientToServer, packetNum, timestamp);
            }
        } catch (Exception e) {
            Logs.GENERAL.warn(e, "Failed to read pcap record",
                "num", packetNum);
        }
    }
}
