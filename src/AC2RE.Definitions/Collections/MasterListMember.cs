namespace AC2RE.Definitions;

public class MasterListMember : IHeapObject {

    public virtual PackageType packageType => PackageType.MasterListMember;

    public uint enumVal; // mEnum

    public MasterListMember(AC2Reader data) {
        enumVal = data.ReadUInt32();
    }
}
