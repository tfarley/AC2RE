namespace AC2RE.Definitions;

public class SoundInfo {

    // SoundInfo
    public DataId did; // m_DID
    public uint bitfield; // m_bitField
    public float randFreqTop; // m_randFreqTop
    public float randFreqBot; // m_randFreqBot
    public float priority; // m_priority
    public float minDist; // m_minDistance
    public DataId objDid; // m_objID

    public SoundInfo(AC2Reader data) {
        did = data.ReadDataId();
        bitfield = data.ReadUInt32();
        randFreqTop = data.ReadSingle();
        randFreqBot = data.ReadSingle();
        priority = data.ReadSingle();
        minDist = data.ReadSingle();
        objDid = data.ReadDataId();
    }
}
