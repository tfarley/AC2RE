namespace AC2RE.Definitions;

public class WState {

    public DataId did; // m_DID
    public IHeapObject package; // m_pData
    public PackageType packageType; // m_pid

    public WState(AC2Reader data) {
        did = data.ReadDataId();
        uint stateDataLen = data.ReadUInt32();
        package = data.UnpackHeapObject<IHeapObject>(true);
        packageType = (PackageType)data.ReadUInt32();
    }
}
