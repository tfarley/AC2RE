namespace AC2E.Def {

    public class BiasProfile : MasterListMember {

        public override PackageType packageType => PackageType.BiasProfile;

        public int variance; // m_variance
        public int curObjQuality; // m_curObjQuality
        public AAHash biasHash; // m_biashash
        public int quality; // m_quality
        public AAHash overrideHash; // m_overridehash

        public BiasProfile(AC2Reader data) : base(data) {
            variance = data.ReadInt32();
            curObjQuality = data.ReadInt32();
            data.ReadPkg<AAHash>(v => biasHash = v);
            quality = data.ReadInt32();
            data.ReadPkg<AAHash>(v => overrideHash = v);
        }
    }
}
