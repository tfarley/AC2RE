namespace AC2E.Def {

    public class RemoveRecipeEffect : InstantEffect {

        public override PackageType packageType => PackageType.RemoveRecipeEffect;

        public DataId recipeDid; // m_didRecipe

        public RemoveRecipeEffect(AC2Reader data) : base(data) {
            recipeDid = data.ReadDataId();
        }
    }
}
