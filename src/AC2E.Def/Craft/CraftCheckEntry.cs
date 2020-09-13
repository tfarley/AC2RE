namespace AC2E.Def {

    public class CraftCheckEntry : IPackage {

        public PackageType packageType => PackageType.CraftCheckEntry;

        public AList randomThresholds; // m_listRandThresh
        public float threshold; // m_fThresh
        public ARHash<IPackage> randEntries; // m_hashRandEntries

        public CraftCheckEntry(AC2Reader data) {
            data.ReadPkg<AList>(v => randomThresholds = v);
            threshold = data.ReadSingle();
            data.ReadPkg<ARHash<IPackage>>(v => randEntries = v);
        }
    }
}
