namespace AC2E.Def {

    public class StoreGroup : IPackage {

        public PackageType packageType => PackageType.StoreGroup;

        public StringInfo name; // m_siName
        public AArray storeTemplates; // m_storeTemplates
        public int totalSales; // m_iTotalSales
        public int maxAccountSales; // m_iMaxAccountSales

        public StoreGroup(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => name = v);
            data.ReadPkg<AArray>(v => storeTemplates = v);
            totalSales = data.ReadInt32();
            maxAccountSales = data.ReadInt32();
        }
    }
}
