using System.Collections.Generic;

namespace AC2E.Def {

    public class RecipeDifficultyTable : IPackage {

        public PackageType packageType => PackageType.RecipeDifficultyTable;

        public List<uint> map; // m_map
        public int maxDiff; // m_maxDiff
        public int minDiff; // m_minDiff

        public RecipeDifficultyTable(AC2Reader data) {
            data.ReadPkg<AArray>(v => map = v);
            maxDiff = data.ReadInt32();
            minDiff = data.ReadInt32();
        }
    }
}
