namespace AC2E.Def {

    public class ProduceRecipeAction : IPackage {

        public PackageType packageType => PackageType.ProduceRecipeAction;

        public uint ordinal; // m_uiOrdinal
        public uint flags; // m_flags
        public DataId entityDid; // m_entityDID
        public DataId mappingTableDid; // m_didMappingTable
        public uint quantity; // m_uiQuantity

        public ProduceRecipeAction(AC2Reader data) {
            ordinal = data.ReadUInt32();
            flags = data.ReadUInt32();
            entityDid = data.ReadDataId();
            mappingTableDid = data.ReadDataId();
            quantity = data.ReadUInt32();
        }
    }
}
