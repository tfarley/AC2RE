namespace AC2E.Def {

    public class MusicInfo {

        // Const *_MusicType
        public enum MusicType : uint {
            ENC_WAV = 0x40000001,
            SCRIPT = 0x40000002,
            SEGMENT = 0x80000001,
            STYLE = 0x80000002,
            CHORD_MAP = 0x80000003,
            BAND = 0x80000004,
            TEMPLATE = 0x80000005,
            DLS = 0x80000006,
            FILE_NAME = 0x80000007,
            MOTIF = 0x80000008,
            WAV = 0x80000009,
            AUDIO_PATH = 0x8000000A,
        }

        public DataId did; // m_DID
        public uint bitField; // m_bitField
        public float priority; // m_priority
        public float minDistance; // m_minDistance
        public MusicType type; // m_type
        public DataId objDid; // m_objID

        public MusicInfo(AC2Reader data) {
            did = data.ReadDataId();
            bitField = data.ReadUInt32();
            priority = data.ReadSingle();
            minDistance = data.ReadSingle();
            type = (MusicType)data.ReadUInt32();
            objDid = data.ReadDataId();
        }
    }
}
