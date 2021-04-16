namespace AC2RE.Definitions {

    public class AllegianceControl : IPackage {

        public PackageType packageType => PackageType.AllegianceControl;

        public uint pad1;
        public uint pad2;
        public uint pad3;
        public uint pad4;
        public uint pad5;
        public uint pad6;
        public uint pad7;
        public uint pad8;
        public uint pad9;
        public uint pad10;
        public uint pad11;
        public uint pad12;
        public uint pad13;
        public uint pad14;
        public uint pad15;
        public uint pad16;
        public uint pad17;
        public uint pad18;
        public uint pad19;
        public uint pad20;
        public uint pad21;
        public uint pad22;
        public uint pad23;
        public uint pad24;
        public uint pad25;
        public uint pad26;
        public uint pad27;
        public uint pad28;
        public uint pad29;
        public uint pad30;
        public uint pad31;

        public AllegianceControl(AC2Reader data) {
            pad1 = data.ReadUInt32();
            pad2 = data.ReadUInt32();
            pad3 = data.ReadUInt32();
            pad4 = data.ReadUInt32();
            pad5 = data.ReadUInt32();
            pad6 = data.ReadUInt32();
            pad7 = data.ReadUInt32();
            pad8 = data.ReadUInt32();
            pad9 = data.ReadUInt32();
            pad10 = data.ReadUInt32();
            pad11 = data.ReadUInt32();
            pad12 = data.ReadUInt32();
            pad13 = data.ReadUInt32();
            pad14 = data.ReadUInt32();
            pad15 = data.ReadUInt32();
            pad16 = data.ReadUInt32();
            pad17 = data.ReadUInt32();
            pad18 = data.ReadUInt32();
            pad19 = data.ReadUInt32();
            pad20 = data.ReadUInt32();
            pad21 = data.ReadUInt32();
            pad22 = data.ReadUInt32();
            pad23 = data.ReadUInt32();
            pad24 = data.ReadUInt32();
            pad25 = data.ReadUInt32();
            pad26 = data.ReadUInt32();
            pad27 = data.ReadUInt32();
            pad28 = data.ReadUInt32();
            pad29 = data.ReadUInt32();
            pad30 = data.ReadUInt32();
            pad31 = data.ReadUInt32();
        }
    }
}
