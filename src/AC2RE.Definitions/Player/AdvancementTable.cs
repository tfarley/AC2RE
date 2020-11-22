using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class AdvancementTable : IPackage {

        public PackageType packageType => PackageType.AdvancementTable;

        public List<ulong> map; // m_map
        public int maxLevel; // mMaxLevel
        public WPString name; // mName

        public AdvancementTable(AC2Reader data) {
            data.ReadPkg<LArray>(v => map = v);
            maxLevel = data.ReadInt32();
            data.ReadPkg<WPString>(v => name = v);
        }
    }
}
