namespace AC2RE.Definitions;

public class FlagRecipeAction : IHeapObject {

    public PackageType packageType => PackageType.FlagRecipeAction;

    public uint type; // m_type
    public uint ordinal; // m_uiOrdinal
    public uint flags; // m_flags
    public uint minSpinnerVal; // m_uiMinSpinnerVal
    public uint maxSpinnerVal; // m_uiMaxSpinnerVal

    public FlagRecipeAction(AC2Reader data) {
        type = data.ReadUInt32();
        ordinal = data.ReadUInt32();
        flags = data.ReadUInt32();
        minSpinnerVal = data.ReadUInt32();
        maxSpinnerVal = data.ReadUInt32();
    }
}
