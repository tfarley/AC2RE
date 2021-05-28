using System;

namespace AC2RE.Definitions {

    public class ProduceRecipeAction : IPackage {

        public PackageType packageType => PackageType.ProduceRecipeAction;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            OBJECT_TO_PRODUCE = 1 << 0, // 0x00000001, ProduceRecipeAction::SetObjectToProduce / ProduceRecipeAction::SetObjectToProduceByDID
            OBJECT_TO_PRODUCE_DYNAMIC_QUANTITY = 1 << 1, // 0x00000002, ProduceRecipeAction::SetObjectToProduceDynamicQuantity
        }

        public uint ordinal; // m_uiOrdinal
        public Flag flags; // m_flags
        public DataId entityDid; // m_entityDID
        public DataId mappingTableDid; // m_didMappingTable
        public uint quantity; // m_uiQuantity

        public ProduceRecipeAction(AC2Reader data) {
            ordinal = data.ReadUInt32();
            flags = (Flag)data.ReadUInt32();
            entityDid = data.ReadDataId();
            mappingTableDid = data.ReadDataId();
            quantity = data.ReadUInt32();
        }
    }
}
