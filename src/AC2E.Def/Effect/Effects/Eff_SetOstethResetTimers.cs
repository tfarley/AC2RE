namespace AC2E.Def {

    public class Eff_SetOstethResetTimers : Effect {

        public override PackageType packageType => PackageType.Eff_SetOstethResetTimers;

        public AList recipes; // m_recipeList

        public Eff_SetOstethResetTimers(AC2Reader data) : base(data) {
            data.ReadPkg<AList>(v => recipes = v);
        }
    }
}
