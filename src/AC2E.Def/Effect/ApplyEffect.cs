namespace AC2E.Def {

    public class ApplyEffect : Effect {

        public override PackageType packageType => PackageType.ApplyEffect;

        public SingletonPkg<Effect> effect; // m_effect
        public uint effectCategory; // m_effectCategory

        public ApplyEffect(AC2Reader data) : base(data) {
            data.ReadPkg<SingletonPkg<IPackage>>(v => effect = v.to<Effect>());
            effectCategory = data.ReadUInt32();
        }
    }
}
