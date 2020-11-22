namespace AC2RE.Definitions {

    public class TargetLoreFilter : IPackage {

        public PackageType packageType => PackageType.TargetLoreFilter;

        public int minLore; // m_iMinLore
        public int maxLore; // m_iMaxLore

        public TargetLoreFilter(AC2Reader data) {
            minLore = data.ReadInt32();
            maxLore = data.ReadInt32();
        }
    }
}
