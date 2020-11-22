namespace AC2RE.Definitions {

    public class AIPetEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.AIPetEffect;

        public uint petClass; // m_petClass

        public AIPetEffect(AC2Reader data) : base(data) {
            petClass = data.ReadUInt32();
        }
    }
}
