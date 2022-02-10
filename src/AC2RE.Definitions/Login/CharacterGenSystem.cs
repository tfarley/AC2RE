namespace AC2RE.Definitions;

public class CharacterGenSystem : IHeapObject {

    public PackageType packageType => PackageType.CharacterGenSystem;

    public DataId playerEntityDid; // m_playerEntityDID
    public DataId adminEntityDid; // m_adminEntityDID

    public CharacterGenSystem(AC2Reader data) {
        playerEntityDid = data.ReadDataId();
        adminEntityDid = data.ReadDataId();
    }
}
