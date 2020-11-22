namespace AC2RE.Definitions {

    public class FellowshipControl : IPackage {

        public PackageType packageType => PackageType.FellowshipControl;

        public uint unk1;
        public uint unk2;
        public uint unk3;
        public uint unk4;
        public DataId fellowMarkerDid; // DIDFellowMarker_FellowGlobals
        public uint unk5;
        public uint unk6;
        public uint unk7;
        public uint unk8;
        public uint unk9;
        public uint unk10;
        public uint unk11;
        public uint unk12;
        public uint unk13;

        public FellowshipControl(AC2Reader data) {
            unk1 = data.ReadUInt32();
            unk2 = data.ReadUInt32();
            unk3 = data.ReadUInt32();
            unk4 = data.ReadUInt32();
            fellowMarkerDid = data.ReadDataId();
            unk5 = data.ReadUInt32();
            unk6 = data.ReadUInt32();
            unk7 = data.ReadUInt32();
            unk8 = data.ReadUInt32();
            unk9 = data.ReadUInt32();
            unk10 = data.ReadUInt32();
            unk11 = data.ReadUInt32();
            unk12 = data.ReadUInt32();
            unk13 = data.ReadUInt32();
        }
    }
}
