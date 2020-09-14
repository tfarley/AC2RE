namespace AC2E.Def {

    public class FineItemClassFilter : IPackage {

        public PackageType packageType => PackageType.FineItemClassFilter;

        public AList itemClasses; // m_itemClasses

        public FineItemClassFilter(AC2Reader data) {
            data.ReadPkg<AList>(v => itemClasses = v);
        }
    }
}
