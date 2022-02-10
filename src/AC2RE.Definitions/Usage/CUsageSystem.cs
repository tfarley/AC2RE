using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CUsageSystem : IHeapObject {

    public PackageType packageType => PackageType.CUsageSystem;

    public List<ulong> itemUseCache; // m_itemUseCache

    public CUsageSystem(AC2Reader data) {
        data.ReadHO<LList>(v => itemUseCache = v);
    }
}
