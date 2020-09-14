namespace AC2E.Def {

    public class ClassFilter : IPackage {

        public PackageType packageType => PackageType.ClassFilter;

        public AList classPids; // m_classPIDs

        public ClassFilter(AC2Reader data) {
            data.ReadPkg<AList>(v => classPids = v);
        }
    }
}
