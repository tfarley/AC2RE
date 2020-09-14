namespace AC2E.Def {

    public class Eff_UseXPStone : Effect {

        public override PackageType packageType => PackageType.Eff_UseXPStone;

        public SingletonPkg<Effect> completeEffect; // m_effComplete
        public SingletonPkg<Effect> bestowEffect; // m_effBestow

        public Eff_UseXPStone(AC2Reader data) : base(data) {
            data.ReadSingletonPkg<Effect>(v => completeEffect = v);
            data.ReadSingletonPkg<Effect>(v => bestowEffect = v);
        }
    }
}
