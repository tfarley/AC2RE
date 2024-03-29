﻿using System;

namespace AC2RE.Definitions;

public class CContinSoundInfo {

    // Enum CContinSoundInfo::CContinSound_Flag
    [Flags]
    public enum Flag : uint {
        NONE = 0, // CSF_NONE
        SOUNDINFO = 1 << 0, // CSF_SOUNDINFO 0x00000001
        MUSICINFO = 1 << 1, // CSF_MUSICINFO 0x00000002
        DOMINANT = 1 << 2, // CSF_DOMINANT 0x00000004
        SELFSTOP = 1 << 3, // CSF_SELFSTOP 0x00000008
        COMPLEX = 1 << 4, // CSF_COMPLEX 0x00000010
    }

    // CContinSoundInfo
    public Flag bitfield; // m_bitField
    public DataId objDid; // m_obj_did
    public float volume; // m_volume
    public float minPitch; // m_min_pitch
    public float maxPitch; // m_max_pitch
    public float minHeight; // m_min_height
    public float maxHeight; // m_max_height

    public CContinSoundInfo(AC2Reader data) {
        bitfield = data.ReadEnum<Flag>();
        objDid = data.ReadDataId();
        volume = data.ReadSingle();
        minPitch = data.ReadSingle();
        maxPitch = data.ReadSingle();
        minHeight = data.ReadSingle();
        maxHeight = data.ReadSingle();
    }
}
