namespace AC2E.Def {

    public class Eff_Com_Hero_Perk_WildMagic1 : Effect {

        public override PackageType packageType => PackageType.Eff_Com_Hero_Perk_WildMagic1;

        public RandomSelectionTable randomEffects; // m_randomEffects

        public Eff_Com_Hero_Perk_WildMagic1(AC2Reader data) : base(data) {
            data.ReadPkg<RandomSelectionTable>(v => randomEffects = v);
        }
    }
}
