namespace AC2RE.Definitions;

public class HookData : IHeapObject {

    public virtual PackageType packageType => PackageType.HookData;

    public uint hookType; // mHookType

    public HookData(AC2Reader data) {
        hookType = data.ReadUInt32();
    }
}
