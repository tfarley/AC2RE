namespace AC2E.Def {

    public class RecipeCostTable : IPackage {

        public PackageType packageType => PackageType.RecipeCostTable;

        public uint maxLevel; // m_maxLevel
        public ARHash<RecipeCostData> map; // m_map

        public RecipeCostTable(AC2Reader data) {
            maxLevel = data.ReadUInt32();
            data.ReadPkg<ARHash<IPackage>>(v => map = v.to<RecipeCostData>());
        }
    }
}
