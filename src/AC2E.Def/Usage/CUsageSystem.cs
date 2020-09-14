namespace AC2E.Def {

    public class CUsageSystem : IPackage {

        public PackageType packageType => PackageType.CUsageSystem;

        public LList itemUseCache; // m_itemUseCache

        public CUsageSystem(AC2Reader data) {
            data.ReadPkg<LList>(v => itemUseCache = v);
        }
    }
}
