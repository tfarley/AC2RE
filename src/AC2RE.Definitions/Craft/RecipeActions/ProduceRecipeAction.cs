using System;

namespace AC2RE.Definitions;

public class ProduceRecipeAction : IHeapObject {

    public PackageType packageType => PackageType.ProduceRecipeAction;

    // WLib ProduceRecipeAction
    [Flags]
    public enum Flag : uint {
        None = 0,
        HasObjectToProduce = 1 << 0, // SetObjectToProduce + SetObjectToProduceByDID 0x00000001
        HasObjectToProduceDynamicQuantity = 1 << 1, // SetObjectToProduceDynamicQuantity 0x00000002
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
