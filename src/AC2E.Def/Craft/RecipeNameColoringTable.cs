using System.Collections.Generic;

namespace AC2E.Def {

    public class RecipeNameColoringTable : IPackage {

        public PackageType packageType => PackageType.RecipeNameColoringTable;

        public List<uint> map; // m_map
        public int maxDiff; // m_maxDiff
        public int minDiff; // m_minDiff

        public RecipeNameColoringTable(AC2Reader data) {
            data.ReadPkg<AArray>(v => map = v);
            maxDiff = data.ReadInt32();
            minDiff = data.ReadInt32();
        }
    }
}
