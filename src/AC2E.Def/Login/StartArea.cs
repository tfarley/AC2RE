namespace AC2E.Def {

    public class StartArea : IPackage {

        public PackageType packageType => PackageType.StartArea;

        public uint startingLocType; // m_startingLocType
        public RList<IPackage> positions; // m_posList
        public WPString description; // m_desc

        public StartArea(AC2Reader data) {
            startingLocType = data.ReadUInt32();
            data.ReadPkg<RList<IPackage>>(v => positions = v);
            data.ReadPkg<WPString>(v => description = v);
        }
    }
}
