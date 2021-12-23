namespace AC2RE.Definitions;

public class StartInvData : IPackage {

    public PackageType packageType => PackageType.StartInvData;

    public uint colorType; // colorType
    public bool equipped; // equipped
    public float modifier; // modifier
    public AppearanceKey appKey; // appKey
    public WPString entityName; // entityName
    public int quantity; // quantity
    public DataId entityDid; // entityDID

    public StartInvData(AC2Reader data) {
        colorType = data.ReadUInt32();
        equipped = data.ReadBoolean();
        modifier = data.ReadSingle();
        appKey = (AppearanceKey)data.ReadUInt32();
        data.ReadPkg<WPString>(v => entityName = v);
        quantity = data.ReadInt32();
        entityDid = data.ReadDataId();
    }
}
