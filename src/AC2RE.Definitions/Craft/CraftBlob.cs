namespace AC2RE.Definitions;

public class CraftBlob : IHeapObject {

    public virtual PackageType packageType => PackageType.CraftBlob;

    public uint difficulty; // m_diff
    public DataId recipeDid; // m_didRecipe
    public DataId entityDid; // m_entityDID

    public CraftBlob(AC2Reader data) {
        difficulty = data.ReadUInt32();
        recipeDid = data.ReadDataId();
        entityDid = data.ReadDataId();
    }
}
