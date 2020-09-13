namespace AC2E.Def {

    public class LevelFilter : StoreFilter {

        public override PackageType packageType => PackageType.LevelFilter;

        public int minLevel; // m_iMinLevel
        public int maxLevel; // m_iMaxLevel

        public LevelFilter(AC2Reader data) : base(data) {
            minLevel = data.ReadInt32();
            maxLevel = data.ReadInt32();
        }
    }
}
