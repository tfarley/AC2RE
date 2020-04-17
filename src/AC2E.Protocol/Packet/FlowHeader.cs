using System.IO;

namespace AC2E.Protocol.Packet {

    public class FlowHeader {

        public uint flowData;
        public ushort interval;

        public FlowHeader(BinaryReader data) {
            flowData = data.ReadUInt32();
            interval = data.ReadUInt16();
        }

        public void write(BinaryWriter data) {
            data.Write(flowData);
            data.Write(interval);
        }
    }
}
