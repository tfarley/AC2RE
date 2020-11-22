namespace AC2RE.Definitions {

    public class BindRecipeAction : IPackage {

        public PackageType packageType => PackageType.BindRecipeAction;

        public uint ordinal; // m_uiOrdinal
        public uint minSpinnerVal; // m_uiMinSpinnerVal
        public uint maxSpinnerVal; // m_uiMaxSpinnerVal

        public BindRecipeAction(AC2Reader data) {
            ordinal = data.ReadUInt32();
            minSpinnerVal = data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
        }
    }
}
