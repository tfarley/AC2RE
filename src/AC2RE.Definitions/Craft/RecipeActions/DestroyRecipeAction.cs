using System;

namespace AC2RE.Definitions;

public class DestroyRecipeAction : IHeapObject {

    public PackageType packageType => PackageType.DestroyRecipeAction;

    // WLib DestroyRecipeAction
    [Flags]
    public enum Flag : uint {
        None = 0,
        HasObjectToDestroy = 1 << 0, // SetObjectToDestroy 0x00000001
        HasObjectToDestroyDynamicQuantity = 1 << 1, // SetObjectToDestroyDynamicQuantity 0x00000002
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
