using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CAmbientSoundInfo {

    public uint soundType; // m_sound_type
    public DataId envDid; // m_env_did
    public List<CContinSoundInfo> continuousSounds; // m_contin_sounds
    public List<CIntermitSoundInfo> intermittentSounds; // m_intermit_sounds

    public CAmbientSoundInfo(AC2Reader data) {
        soundType = data.ReadUInt32();
        envDid = data.ReadDataId();
        intermittentSounds = data.ReadList(() => new CIntermitSoundInfo(data));
        continuousSounds = data.ReadList(() => new CContinSoundInfo(data));
    }
}
