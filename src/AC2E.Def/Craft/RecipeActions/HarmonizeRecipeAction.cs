namespace AC2E.Def {

    public class HarmonizeRecipeAction : IPackage {

        public PackageType packageType => PackageType.HarmonizeRecipeAction;

        public uint ordinal; // m_uiOrdinal
        public uint minSpinnerVal; // m_uiMinSpinnerVal
        public uint maxSpinnerVal; // m_uiMaxSpinnerVal

        public HarmonizeRecipeAction(AC2Reader data) {
            ordinal = data.ReadUInt32();
            minSpinnerVal = data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
        }
    }
}
