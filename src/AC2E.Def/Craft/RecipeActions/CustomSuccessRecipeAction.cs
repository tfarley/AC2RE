namespace AC2E.Def {

    public class CustomSuccessRecipeAction : RecipeAction {

        public override PackageType packageType => PackageType.CustomSuccessRecipeAction;

        public uint ordinal; // m_uiOrdinal
        public float param; // m_fParam

        public CustomSuccessRecipeAction(AC2Reader data) : base(data) {
            ordinal = data.ReadUInt32();
            param = data.ReadSingle();
        }
    }
}
