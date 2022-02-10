using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PackageIDTable : IHeapObject {

    public PackageType packageType => PackageType.PackageIDTable;

    public Dictionary<uint, uint> pidHash; // m_pidHash

    public PackageIDTable(AC2Reader data) {
        data.ReadHO<AAHash>(v => pidHash = v);
    }
}
