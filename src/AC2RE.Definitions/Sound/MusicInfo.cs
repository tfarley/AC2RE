namespace AC2RE.Definitions;

public class MusicInfo {

    // MusicInfo
    public DataId did; // m_DID
    public uint bitfield; // m_bitField
    public float priority; // m_priority
    public float minDist; // m_minDistance
    public MusicType type; // m_type
    public DataId objDid; // m_objID

    public MusicInfo(AC2Reader data) {
        did = data.ReadDataId();
        bitfield = data.ReadUInt32();
        priority = data.ReadSingle();
        minDist = data.ReadSingle();
        type = (MusicType)data.ReadUInt32();
        objDid = data.ReadDataId();
    }
}
