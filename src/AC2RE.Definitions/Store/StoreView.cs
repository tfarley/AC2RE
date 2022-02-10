using System.Collections.Generic;

namespace AC2RE.Definitions;

public class StoreView : IHeapObject {

    public PackageType packageType => PackageType.StoreView;

    public List<SaleProfile> saleProfiles; // m_SaleProfiles
    public DataId templateDid; // m_didTemplate
    public int storeSize; // m_iStoreSize
    public int pos; // m_iPos

    public StoreView() {

    }

    public StoreView(AC2Reader data) {
        data.ReadHO<RList>(v => saleProfiles = v.to<SaleProfile>());
        templateDid = data.ReadDataId();
        storeSize = data.ReadInt32();
        pos = data.ReadInt32();
    }

    public void write(AC2Writer data) {
        data.WriteHO(RList.from(saleProfiles));
        data.Write(templateDid);
        data.Write(storeSize);
        data.Write(pos);
    }
}
