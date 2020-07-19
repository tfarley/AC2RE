using System.IO;

namespace AC2E.Def {

    public class StoreView : IPackage {

        public PackageType packageType => PackageType.StoreView;

        public RList<SaleProfile> m_SaleProfiles;
        public DataId m_didTemplate;
        public int m_iStoreSize;
        public int m_iPos;

        public StoreView() {

        }

        public StoreView(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<RList<IPackage>>(v => m_SaleProfiles = v.to<SaleProfile>(), registry);
            m_didTemplate = data.ReadDataId();
            m_iStoreSize = data.ReadInt32();
            m_iPos = data.ReadInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_SaleProfiles, registry);
            data.Write(m_didTemplate);
            data.Write(m_iStoreSize);
            data.Write(m_iPos);
        }
    }
}
