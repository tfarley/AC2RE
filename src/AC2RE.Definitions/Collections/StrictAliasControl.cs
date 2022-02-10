using System.Collections.Generic;

namespace AC2RE.Definitions;

public class StrictAliasControl : IHeapObject {

    public PackageType packageType => PackageType.StrictAliasControl;

    public Dictionary<IHeapObject, IHeapObject> strictAliasTable; // m_strictAliasTable

    public StrictAliasControl(AC2Reader data) {
        data.ReadHO<NRHash>(v => strictAliasTable = v);
    }
}
