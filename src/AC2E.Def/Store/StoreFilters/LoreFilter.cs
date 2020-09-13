namespace AC2E.Def {

    public class LoreFilter : StoreFilter {

        public override PackageType packageType => PackageType.LoreFilter;

        public int minLore; // m_iMinLore
        public int maxLore; // m_iMaxLore

        public LoreFilter(AC2Reader data) : base(data) {
            minLore = data.ReadInt32();
            maxLore = data.ReadInt32();
        }
    }
}
