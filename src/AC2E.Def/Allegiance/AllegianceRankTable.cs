namespace AC2E.Def {

    public class AllegianceRankTable : IPackage {

        public PackageType packageType => PackageType.AllegianceRankTable;

        public bool setupOK; // m_setupOK
        public ARHash<StringInfo> rankHash; // m_RankHash

        public AllegianceRankTable(AC2Reader data) {
            setupOK = data.ReadBoolean();
            data.ReadPkg<ARHash<IPackage>>(v => rankHash = v.to<StringInfo>());
        }
    }
}
