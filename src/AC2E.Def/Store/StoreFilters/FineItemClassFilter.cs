namespace AC2E.Def {

    public class FineItemClassFilter : StoreFilter {

        public override PackageType packageType => PackageType.FineItemClassFilter;

        public AList itemClasses; // m_itemClasses

        public FineItemClassFilter(AC2Reader data) : base(data) {
            data.ReadPkg<AList>(v => itemClasses = v);
        }
    }
}
