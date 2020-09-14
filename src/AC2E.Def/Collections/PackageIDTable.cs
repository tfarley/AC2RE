namespace AC2E.Def {

    public class PackageIDTable : IPackage {

        public PackageType packageType => PackageType.PackageIDTable;

        public AAHash pidHash; // m_pidHash

        public PackageIDTable(AC2Reader data) {
            data.ReadPkg<AAHash>(v => pidHash = v);
        }
    }
}
