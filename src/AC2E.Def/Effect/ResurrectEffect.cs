namespace AC2E.Def {

    public class ResurrectEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.ResurrectEffect;

        public FxId rezFx; // m_rezFX

        public ResurrectEffect(AC2Reader data) : base(data) {
            rezFx = (FxId)data.ReadUInt32();
        }
    }
}
