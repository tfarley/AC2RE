namespace AC2RE.Definitions {

    public class AppearanceModRecipeAction : IPackage {

        public PackageType packageType => PackageType.AppearanceModRecipeAction;

        public AppearanceKey appKey; // m_key
        public uint ordinal; // m_uiOrdinal
        public float mod; // m_mod
        public uint minSpinnerVal; // m_uiMinSpinnerVal
        public uint maxSpinnerVal; // m_uiMaxSpinnerVal

        public AppearanceModRecipeAction(AC2Reader data) {
            appKey = (AppearanceKey)data.ReadUInt32();
            ordinal = data.ReadUInt32();
            mod = data.ReadSingle();
            minSpinnerVal = data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
        }
    }
}
