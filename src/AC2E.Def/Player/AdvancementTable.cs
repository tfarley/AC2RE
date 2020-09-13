namespace AC2E.Def {

    public class AdvancementTable : IPackage {

        public PackageType packageType => PackageType.AdvancementTable;

        public LArray map; // m_map
        public int maxLevel; // mMaxLevel
        public WPString name; // mName

        public AdvancementTable(AC2Reader data) {
            data.ReadPkg<LArray>(v => map = v);
            maxLevel = data.ReadInt32();
            data.ReadPkg<WPString>(v => name = v);
        }
    }
}
