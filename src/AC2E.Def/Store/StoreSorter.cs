namespace AC2E.Def {

    public class StoreSorter : IPackage {

        public virtual PackageType packageType => PackageType.StoreSorter;

        public StringInfo name; // m_siName

        public StoreSorter(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => name = v);
        }
    }
}
