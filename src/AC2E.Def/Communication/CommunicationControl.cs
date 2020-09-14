namespace AC2E.Def {

    public class CommunicationControl : IPackage {

        public PackageType packageType => PackageType.CommunicationControl;

        public uint unk1;
        public bool m_setupOK; // m_setupOK
        public ARHash<StringInfo> textTypeHash; // m_textTypeHash
        public uint unk2;
        public uint unk3;

        public CommunicationControl(AC2Reader data) {
            unk1 = data.ReadUInt32();
            unk2 = data.ReadUInt32();
            m_setupOK = data.ReadBoolean();
            data.ReadPkg<ARHash<IPackage>>(v => textTypeHash = v.to<StringInfo>());
            unk3 = data.ReadUInt32();
        }
    }
}
