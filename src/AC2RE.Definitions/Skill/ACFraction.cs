namespace AC2RE.Definitions {

    public class ACFraction : IPackage {

        public PackageType packageType => PackageType.ACFraction;

        public IPackage abilityCalculator; // m_ac
        public float val; // m_fVal

        public ACFraction(AC2Reader data) {
            data.ReadPkg<IPackage>(v => abilityCalculator = v);
            val = data.ReadSingle();
        }
    }
}
