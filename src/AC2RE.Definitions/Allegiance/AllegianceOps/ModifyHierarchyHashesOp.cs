namespace AC2RE.Definitions;

public class ModifyHierarchyHashesOp : IPackage {

    public PackageType packageType => PackageType.ModifyHierarchyHashesOp;

    public AllegianceHierarchy hierarchy; // m_hier
    public uint nodesSeen; // m_cNodesSeen
    public bool removeFromHashes; // m_fRemoveFromHashes
    public bool addToHashes; // m_fAddToHashes

    public ModifyHierarchyHashesOp(AC2Reader data) {
        data.ReadPkg<AllegianceHierarchy>(v => hierarchy = v);
        nodesSeen = data.ReadUInt32();
        removeFromHashes = data.ReadBoolean();
        addToHashes = data.ReadBoolean();
    }
}
