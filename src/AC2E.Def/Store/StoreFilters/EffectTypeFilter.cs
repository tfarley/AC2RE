namespace AC2E.Def {

    public class EffectTypeFilter : StoreFilter {

        public override PackageType packageType => PackageType.EntityFilter;

        public AList effectTypes; // m_effectTypes

        public EffectTypeFilter(AC2Reader data) : base(data) {
            data.ReadPkg<AList>(v => effectTypes = v);
        }
    }
}
