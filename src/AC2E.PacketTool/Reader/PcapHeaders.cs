using AC2E.Def;

namespace AC2E.PacketTool {

    public class PcapHeader {

        public static readonly uint MAGIC_NUMBER_1 = 0xA1B2C3D4;
        public static readonly uint MAGIC_NUMBER_2 = 0xD4C3B2A1;

        public uint magicNumber;
        public ushort versionMajor;
        public ushort versionMinor;
        public uint thisZone;
        public uint sigFigs;
        public uint snapLen;
        public uint network;

        public PcapHeader(AC2Reader data) {
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

        public PcapRecordHeader(AC2Reader data) {
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

        public PcapngBlockHeader(AC2Reader data) {
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
}
