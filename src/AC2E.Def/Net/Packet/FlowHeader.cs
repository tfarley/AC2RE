using System.IO;

namespace AC2E.Def {

    public class FlowHeader {

        // CFlowStruct
        public uint length; // cbDataRecvd
        public ushort interval; // interval

        public FlowHeader(BinaryReader data) {
            length = data.ReadUInt32();
            interval = data.ReadUInt16();
        }

        public void write(BinaryWriter data) {
            data.Write(length);
            data.Write(interval);
        }
    }
}
