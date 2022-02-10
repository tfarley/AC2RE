using System.Collections.Generic;

namespace AC2RE.Definitions;

public class FineItemClassFilter : IHeapObject {

    public PackageType packageType => PackageType.FineItemClassFilter;

    public List<uint> itemClasses; // m_itemClasses

    public FineItemClassFilter(AC2Reader data) {
        data.ReadHO<AList>(v => itemClasses = v);
    }
}
