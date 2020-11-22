namespace AC2RE.Definitions {

    public class Eff_WildMagic4_EepEars : Effect {

        public override PackageType packageType => PackageType.Eff_WildMagic4_EepEars;

        public RandomSelectionTable randomEffects; // m_randomEffects

        public Eff_WildMagic4_EepEars(AC2Reader data) : base(data) {
            data.ReadPkg<RandomSelectionTable>(v => randomEffects = v);
        }
    }
}
