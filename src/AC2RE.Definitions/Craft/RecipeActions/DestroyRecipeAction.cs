using System;

namespace AC2RE.Definitions {

    public class DestroyRecipeAction : IPackage {

        public PackageType packageType => PackageType.DestroyRecipeAction;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            OBJECT_TO_DESTROY = 1 << 0, // 0x00000001, DestroyRecipeAction::SetObjectToDestroy
            OBJECT_TO_DESTROY_DYNAMIC_QUANTITY = 1 << 1, // 0x00000002, DestroyRecipeAction::SetObjectToDestroyDynamicQuantity
        }

        public uint ordinal; // m_uiOrdinal
        public Flag flags; // m_flags
        public DataId mappingTableDid; // m_didMappingTable
        public uint minSpinnerVal; // m_uiMinSpinnerVal
        public uint quantity; // m_uiQuantity
        public uint maxSpinnerVal; // m_uiMaxSpinnerVal

        public DestroyRecipeAction(AC2Reader data) {
            ordinal = data.ReadUInt32();
            flags = (Flag)data.ReadUInt32();
            mappingTableDid = data.ReadDataId();
            minSpinnerVal = data.ReadUInt32();
            quantity = data.ReadUInt32();
            maxSpinnerVal = data.ReadUInt32();
        }
    }
}
