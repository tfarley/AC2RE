namespace AC2RE.Definitions;

public class ApplyEffect : Effect {

    public override PackageType packageType => PackageType.ApplyEffect;

    public SingletonPkg<Effect> effect; // m_effect
    public uint effectCategory; // m_effectCategory

    public ApplyEffect(AC2Reader data) : base(data) {
        data.ReadPkg<Effect>(v => effect = v);
        effectCategory = data.ReadUInt32();
    }
}
