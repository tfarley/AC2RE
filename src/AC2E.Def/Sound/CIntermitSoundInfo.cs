﻿using System;

namespace AC2E.Def {

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

        public Flag bitfield; // m_bitField
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
            bitfield = (Flag)data.ReadUInt32();
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
}
