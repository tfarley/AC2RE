namespace AC2E.Def {

    public class StampRecipeAction : RecipeAction {

        public override PackageType packageType => PackageType.StampRecipeAction;

        public uint ordinal; // m_uiOrdinal
        public uint minSpinnerVal; // m_uiMinSpinnerVal
        public uint maxSpinnerVal; // m_uiMaxSpinnerVal

        public StampRecipeAction(AC2Reader data) : base(data) {
            ordinal = data.ReadUInt32();
            minSpinnerVal = data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
        }
    }
}
