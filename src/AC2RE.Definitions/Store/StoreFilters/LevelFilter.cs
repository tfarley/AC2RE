namespace AC2RE.Definitions {

    public class LevelFilter : IPackage {

        public PackageType packageType => PackageType.LevelFilter;

        public int minLevel; // m_iMinLevel
        public int maxLevel; // m_iMaxLevel

        public LevelFilter(AC2Reader data) {
            minLevel = data.ReadInt32();
            maxLevel = data.ReadInt32();
        }
    }
}
