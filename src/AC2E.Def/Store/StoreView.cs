using System.Collections.Generic;

namespace AC2E.Def {

    public class StoreView : IPackage {

        public PackageType packageType => PackageType.StoreView;

        public List<SaleProfile> saleProfiles; // m_SaleProfiles
        public DataId templateDid; // m_didTemplate
        public int storeSize; // m_iStoreSize
        public int pos; // m_iPos

        public StoreView() {

        }

        public StoreView(AC2Reader data) {
            data.ReadPkg<RList>(v => saleProfiles = v.to<SaleProfile>());
            templateDid = data.ReadDataId();
            storeSize = data.ReadInt32();
            pos = data.ReadInt32();
        }

        public void write(AC2Writer data) {
            data.WritePkg(RList.from(saleProfiles));
            data.Write(templateDid);
            data.Write(storeSize);
            data.Write(pos);
        }
    }
}
