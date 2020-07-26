namespace AC2E.Def {

    public class SoundInfo {

        public DataId did; // m_DID
        public uint bitField; // m_bitField
        public float randFreqTop; // m_randFreqTop
        public float randFreqBot; // m_randFreqBot
        public float priority; // m_priority
        public float minDistance; // m_minDistance
        public DataId objDid; // m_objID

        public SoundInfo(AC2Reader data) {
            did = data.ReadDataId();
            bitField = data.ReadUInt32();
            randFreqTop = data.ReadSingle();
            randFreqBot = data.ReadSingle();
            priority = data.ReadSingle();
            minDistance = data.ReadSingle();
            objDid = data.ReadDataId();
        }
    }
}
