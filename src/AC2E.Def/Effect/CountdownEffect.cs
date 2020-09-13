namespace AC2E.Def {

    public class CountdownEffect : Effect {

        public override PackageType packageType => PackageType.CountdownEffect;

        public SingletonPkg<Effect> resultEffect; // m_effResult

        public CountdownEffect(AC2Reader data) : base(data) {
            data.ReadSingletonPkg(v => resultEffect = v.to<Effect>());
        }
    }
}
