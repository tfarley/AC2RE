using System.Collections.Generic;

namespace AC2RE.Definitions;

public class FineItemClassFilter : IPackage {

    public PackageType packageType => PackageType.FineItemClassFilter;

    public List<uint> itemClasses; // m_itemClasses

    public FineItemClassFilter(AC2Reader data) {
        data.ReadPkg<AList>(v => itemClasses = v);
    }
}
