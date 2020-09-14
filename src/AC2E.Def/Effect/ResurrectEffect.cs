namespace AC2E.Def {

    public class ResurrectEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.ResurrectEffect;

        public uint rezFx; // m_rezFX

        public ResurrectEffect(AC2Reader data) : base(data) {
            rezFx = data.ReadUInt32();
        }
    }
}
