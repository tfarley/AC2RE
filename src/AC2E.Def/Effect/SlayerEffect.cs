namespace AC2E.Def {

    public class SlayerEffect : InstantEffect {

        public override PackageType packageType => PackageType.SlayerEffect;

        public AAHash slayerHash; // m_SlayerHash
        public float slayerVariance; // m_fVariance

        public SlayerEffect(AC2Reader data) : base(data) {
            data.ReadPkg<AAHash>(v => slayerHash = v);
            slayerVariance = data.ReadSingle();
        }
    }
}
