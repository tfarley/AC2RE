namespace AC2E.Def {

    public class Eff_ResetOstethResetTimers : Effect {

        public override PackageType packageType => PackageType.Eff_ResetOstethResetTimers;

        public AList recipes; // m_recipeList

        public Eff_ResetOstethResetTimers(AC2Reader data) : base(data) {
            data.ReadPkg<AList>(v => recipes = v);
        }
    }
}
