using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CSoundDesc {

    public DataId did; // m_DID
    public uint version; // m_version
    public List<CAmbientSoundInfo> sounds; // m_sounds

    public CSoundDesc(AC2Reader data) {
        did = data.ReadDataId();
        version = data.ReadUInt32();
        sounds = data.ReadList(() => new CAmbientSoundInfo(data));
    }
}
