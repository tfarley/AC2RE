namespace AC2E.Def {

    public class ChatChannelControl : IPackage {

        public PackageType packageType => PackageType.ChatChannelControl;

        public uint unk1;
        public ARHash<StringInfo> regionNameTable; // m_regionNameTable
        public uint unk2;

        public ChatChannelControl(AC2Reader data) {
            unk1 = data.ReadUInt32();
            data.ReadPkg<ARHash<IPackage>>(v => regionNameTable = v.to<StringInfo>());
            unk2 = data.ReadUInt32();
        }
    }
}
