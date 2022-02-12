using System;

namespace AC2RE.Definitions;

public class MutateRecipeAction : IHeapObject {

    public PackageType packageType => PackageType.MutateRecipeAction;

    // WLib MutateRecipeAction
    [Flags]
    public enum Flag : uint {
        None = 0,
        HasObjectToMutate = 1 << 0, // SetObjectToMutate 0x00000001
        HasObjectToMutateDynamicQuality = 1 << 1, // SetObjectToMutateDynamicQuality 0x00000002
    }

    public uint ordinal; // m_uiOrdinal
    public IHeapObject treasureProfile; // m_prof
    public BiasProfile biasProfile; // m_biasProf
    public Flag flags; // m_flags
    public DataId mappingTableDid; // m_didMappingTable
    public uint minSpinnerVal; // m_uiMinSpinnerVal
    public uint maxSpinnerVal; // m_uiMaxSpinnerVal

    public MutateRecipeAction(AC2Reader data) {
        ordinal = data.ReadUInt32();
        // TODO: Unknown type "TreasureProfile" - perhaps newer native type?
        data.ReadHO<IHeapObject>(v => treasureProfile = v);
        data.ReadHO<BiasProfile>(v => biasProfile = v);
        flags = data.ReadEnum<Flag>();
        mappingTableDid = data.ReadDataId();
        minSpinnerVal = data.ReadUInt32();
        maxSpinnerVal = data.ReadUInt32();
    }
}
