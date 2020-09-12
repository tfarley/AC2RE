namespace AC2E.Def {

    public class ParameterizedNumericEffect : Effect {

        public override PackageType packageType => PackageType.ParameterizedNumericEffect;

        public float numericVariance; // m_fVariance
        public RArray<IPackage> magData; // m_magData

        public ParameterizedNumericEffect(AC2Reader data) : base(data) {
            numericVariance = data.ReadSingle();
            data.ReadPkg<RArray<IPackage>>(v => magData = v);
        }
    }
}
