namespace AC2RE.Definitions;

public class CustomSuccessRecipeAction : IHeapObject {

    public PackageType packageType => PackageType.CustomSuccessRecipeAction;

    public uint ordinal; // m_uiOrdinal
    public float param; // m_fParam

    public CustomSuccessRecipeAction(AC2Reader data) {
        ordinal = data.ReadUInt32();
        param = data.ReadSingle();
    }
}
