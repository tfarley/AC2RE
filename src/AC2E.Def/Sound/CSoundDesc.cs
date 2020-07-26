using System;
using System.Collections.Generic;

namespace AC2E.Def {

    public class CSoundDesc {

        public class CContinSoundInfo {

            // Enum CIntermitSoundInfo::CIntermitSound_Flag
            [Flags]
            public enum Flag : uint {
                NONE = 0,
                SOUNDINFO = 1 << 0, // 0x00000001
                MUSICINFO = 1 << 1, // 0x00000002
                DOMINANT = 1 << 2, // 0x00000004
                SELFSTOP = 1 << 3, // 0x00000008
                COMPLEX = 1 << 4, // 0x00000010
            }

            public Flag bitField; // m_bitField
            public DataId objDid; // m_obj_did
            public float volume; // m_volume
            public float minPitch; // m_min_pitch
            public float maxPitch; // m_max_pitch
            public float minHeight; // m_min_height
            public float maxHeight; // m_max_height

            public CContinSoundInfo(AC2Reader data) {
                bitField = (Flag)data.ReadUInt32();
                objDid = data.ReadDataId();
                volume = data.ReadSingle();
                minPitch = data.ReadSingle();
                maxPitch = data.ReadSingle();
                minHeight = data.ReadSingle();
                maxHeight = data.ReadSingle();
            }
        }

        public class CIntermitSoundInfo {

            // Enum CIntermitSoundInfo::CIntermitSound_Flag
            [Flags]
            public enum Flag : uint {
                NONE = 0,
                SOUNDINFO = 1 << 0, // 0x00000001
                MUSICINFO = 1 << 1, // 0x00000002
                DOMINANT = 1 << 2, // 0x00000004
                SELFSTOP = 1 << 3, // 0x00000008
            }

            public Flag bitField; // m_bitField
            public DataId objDid; // m_obj_did
            public float volume; // m_volume
            public float probability; // m_probability
            public float minPitch; // m_min_pitch
            public float maxPitch; // m_max_pitch
            public float minHeight; // m_min_height
            public float maxHeight; // m_max_height
            public float minTime; // m_min_time
            public float maxTime; // m_max_time

            public CIntermitSoundInfo(AC2Reader data) {
                bitField = (Flag)data.ReadUInt32();
                objDid = data.ReadDataId();
                volume = data.ReadSingle();
                probability = data.ReadSingle();
                minPitch = data.ReadSingle();
                maxPitch = data.ReadSingle();
                minHeight = data.ReadSingle();
                maxHeight = data.ReadSingle();
                minTime = data.ReadSingle();
                maxTime = data.ReadSingle();
            }
        }

        public class CAmbientSoundInfo {

            public uint soundType; // m_sound_type
            public DataId envDid; // m_env_did
            public List<CContinSoundInfo> continSounds; // m_contin_sounds
            public List<CIntermitSoundInfo> intermitSounds; // m_intermit_sounds

            public CAmbientSoundInfo(AC2Reader data) {
                soundType = data.ReadUInt32();
                envDid = data.ReadDataId();
                intermitSounds = data.ReadList(() => new CIntermitSoundInfo(data));
                continSounds = data.ReadList(() => new CContinSoundInfo(data));
            }
        }

        public DataId did; // m_DID
        public uint version; // m_version
        public List<CAmbientSoundInfo> sounds; // m_sounds

        public CSoundDesc(AC2Reader data) {
            did = data.ReadDataId();
            version = data.ReadUInt32();
            sounds = data.ReadList(() => new CAmbientSoundInfo(data));
        }
    }
}
