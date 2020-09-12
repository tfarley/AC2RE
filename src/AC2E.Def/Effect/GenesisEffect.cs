namespace AC2E.Def {

    public class GenesisEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.GenesisEffect;

        public GenesisEffect(AC2Reader data) : base(data) {

        }
    }
}
