namespace AC2E.Def {

    public class DestroyRecipeAction : RecipeAction {

        public override PackageType packageType => PackageType.DestroyRecipeAction;

        public uint ordinal; // m_uiOrdinal
        public uint flags; // m_flags
        public DataId mappingTableDid; // m_didMappingTable
        public uint minSpinnerVal; // m_uiMinSpinnerVal
        public uint quantity; // m_uiQuantity
        public uint maxSpinnerVal; // m_uiMaxSpinnerVal

        public DestroyRecipeAction(AC2Reader data) : base(data) {
            ordinal = data.ReadUInt32();
            flags = data.ReadUInt32();
            mappingTableDid = data.ReadDataId();
            minSpinnerVal = data.ReadUInt32();
            quantity = data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
        }
    }
}
