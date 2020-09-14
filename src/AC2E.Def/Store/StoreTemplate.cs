namespace AC2E.Def {

    public class StoreTemplate : IPackage {

        public PackageType packageType => PackageType.StoreTemplate;

        public RList<IPackage> filters; // m_filters
        public SingletonPkg<StoreSorter> sorter; // m_sorter
        public StringInfo name; // m_siName
        public StringInfo description; // m_siDesc
        public int totalSales; // m_iTotalSales
        public RArray<IPackage> sales; // m_sales
        public DataId portraitDid; // m_didPortrait
        public int version; // m_iVersion
        public uint flags; // m_uiFlags

        public StoreTemplate(AC2Reader data) {
            data.ReadPkg<RList<IPackage>>(v => filters = v);
            data.ReadSingletonPkg<StoreSorter>(v => sorter = v);
            data.ReadPkg<StringInfo>(v => name = v);
            data.ReadPkg<StringInfo>(v => description = v);
            totalSales = data.ReadInt32();
            data.ReadPkg<RArray<IPackage>>(v => sales = v);
            portraitDid = data.ReadDataId();
            version = data.ReadInt32();
            flags = data.ReadUInt32();
        }
    }
}
