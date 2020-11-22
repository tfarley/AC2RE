using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class AllegianceRankTable : IPackage {

        public PackageType packageType => PackageType.AllegianceRankTable;

        public bool setupOK; // m_setupOK
        public Dictionary<uint, StringInfo> rankHash; // m_RankHash

        public AllegianceRankTable(AC2Reader data) {
            setupOK = data.ReadBoolean();
            data.ReadPkg<ARHash>(v => rankHash = v.to<uint, StringInfo>());
        }
    }
}
