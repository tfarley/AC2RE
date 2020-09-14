namespace AC2E.Def {

    public class StrictAliasControl : IPackage {

        public PackageType packageType => PackageType.StrictAliasControl;

        public NRHash<IPackage, IPackage> strictAliasTable; // m_strictAliasTable

        public StrictAliasControl(AC2Reader data) {
            data.ReadPkg<NRHash<IPackage, IPackage>>(v => strictAliasTable = v);
        }
    }
}
