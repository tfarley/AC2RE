namespace AC2RE.Definitions {

    public class RemoveRecipeEffect : Effect {

        public override PackageType packageType => PackageType.RemoveRecipeEffect;

        public DataId recipeDid; // m_didRecipe

        public RemoveRecipeEffect(AC2Reader data) : base(data) {
            recipeDid = data.ReadDataId();
        }
    }
}
