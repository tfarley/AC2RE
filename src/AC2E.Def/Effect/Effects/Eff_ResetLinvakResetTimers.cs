namespace AC2E.Def {

    public class Eff_ResetLinvakResetTimers : Effect {

        public override PackageType packageType => PackageType.Eff_ResetLinvakResetTimers;

        public AList recipes; // m_recipeList

        public Eff_ResetLinvakResetTimers(AC2Reader data) : base(data) {
            data.ReadPkg<AList>(v => recipes = v);
        }
    }
}
