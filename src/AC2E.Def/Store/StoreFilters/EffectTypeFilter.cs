namespace AC2E.Def {

    public class EffectTypeFilter : IPackage {

        public PackageType packageType => PackageType.EntityFilter;

        public AList effectTypes; // m_effectTypes

        public EffectTypeFilter(AC2Reader data) {
            data.ReadPkg<AList>(v => effectTypes = v);
        }
    }
}
