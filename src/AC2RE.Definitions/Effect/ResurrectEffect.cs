namespace AC2RE.Definitions;

public class ResurrectEffect : ParameterizedNumericEffect {

    public override PackageType packageType => PackageType.ResurrectEffect;

    public FxId rezFx; // m_rezFX

    public ResurrectEffect(AC2Reader data) : base(data) {
        rezFx = data.ReadEnum<FxId>();
    }
}
