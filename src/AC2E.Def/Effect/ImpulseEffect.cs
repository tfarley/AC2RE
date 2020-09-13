namespace AC2E.Def {

    public class ImpulseEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.ImpulseEffect;

        public ImpulseEffect(AC2Reader data) : base(data) {

        }
    }
}
