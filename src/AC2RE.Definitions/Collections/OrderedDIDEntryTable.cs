using System.Collections.Generic;

namespace AC2RE.Definitions;

public class OrderedDIDEntryTable : IHeapObject {

    public PackageType packageType => PackageType.OrderedDIDEntryTable;

    public Dictionary<uint, uint> entriesToIndices; // m_hashEntriesToIndices

    public OrderedDIDEntryTable(AC2Reader data) {
        data.ReadHO<AAHash>(v => entriesToIndices = v);
    }
}
