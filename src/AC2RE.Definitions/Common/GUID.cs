namespace AC2RE.Definitions {

    public class GUID {

        public uint data1; // Data1
        public ushort data2; // Data2
        public ushort data3; // Data3
        public byte[] data4; // Data4

        public GUID(AC2Reader data) {
            data1 = data.ReadUInt32();
            data2 = data.ReadUInt16();
            data3 = data.ReadUInt16();
            data4 = data.ReadBytes(8);
        }
    }
}
