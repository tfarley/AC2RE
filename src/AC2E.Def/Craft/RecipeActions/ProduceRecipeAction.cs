namespace AC2E.Def {

    public class ProduceRecipeAction : RecipeAction {

        public override PackageType packageType => PackageType.ProduceRecipeAction;

        public uint ordinal; // m_uiOrdinal
        public uint flags; // m_flags
        public DataId entityDid; // m_entityDID
        public DataId mappingTableDid; // m_didMappingTable
        public uint quantity; // m_uiQuantity

        public ProduceRecipeAction(AC2Reader data) : base(data) {
            ordinal = data.ReadUInt32();
            flags = data.ReadUInt32();
            entityDid = data.ReadDataId();
            mappingTableDid = data.ReadDataId();
            quantity = data.ReadUInt32();
        }
    }
}
