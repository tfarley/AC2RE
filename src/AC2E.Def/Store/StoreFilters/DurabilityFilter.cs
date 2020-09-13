namespace AC2E.Def {

    public class DurabilityFilter : StoreFilter {

        public override PackageType packageType => PackageType.DurabilityFilter;

        public float minDurability; // m_fMinDurability
        public float maxDurability; // m_fMaxDurability

        public DurabilityFilter(AC2Reader data) : base(data) {
            minDurability = data.ReadSingle();
            maxDurability = data.ReadSingle();
        }
    }
}
