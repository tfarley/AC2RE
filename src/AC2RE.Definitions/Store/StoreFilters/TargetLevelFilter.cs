namespace AC2RE.Definitions {

    public class TargetLevelFilter : IPackage {

        public PackageType packageType => PackageType.TargetLevelFilter;

        public int minLevel; // m_iMinLevel
        public int maxLevel; // m_iMaxLevel

        public TargetLevelFilter(AC2Reader data) {
            minLevel = data.ReadInt32();
            maxLevel = data.ReadInt32();
        }
    }
}
