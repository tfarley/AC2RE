namespace AC2E.Def {

    public class LevelMappingTable : IPackage {

        public PackageType packageType => PackageType.LevelMappingTable;

        public bool nonContiguous; // m_bNonContiguous
        public uint maxLevel; // m_maxLevel
        public LArray map; // m_map
        public WPString name; // m_Name
        public uint minLevel; // m_minLevel

        public LevelMappingTable(AC2Reader data) {
            nonContiguous = data.ReadBoolean();
            maxLevel = data.ReadUInt32();
            data.ReadPkg<LArray>(v => map = v);
            data.ReadPkg<WPString>(v => name = v);
            minLevel = data.ReadUInt32();
        }
    }
}
