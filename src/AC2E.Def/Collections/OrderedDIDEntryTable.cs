using System.Collections.Generic;

namespace AC2E.Def {

    public class OrderedDIDEntryTable : IPackage {

        public PackageType packageType => PackageType.OrderedDIDEntryTable;

        public Dictionary<uint, uint> entriesToIndices; // m_hashEntriesToIndices

        public OrderedDIDEntryTable(AC2Reader data) {
            data.ReadPkg<AAHash>(v => entriesToIndices = v);
        }
    }
}
