namespace AC2E.Def {

    public class Eff_SetLinvakResetTimers : Effect {

        public override PackageType packageType => PackageType.Eff_SetLinvakResetTimers;

        public AList recipes; // m_recipeList

        public Eff_SetLinvakResetTimers(AC2Reader data) : base(data) {
            data.ReadPkg<AList>(v => recipes = v);
        }
    }
}
