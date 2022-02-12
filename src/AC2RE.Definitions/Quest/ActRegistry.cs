using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ActRegistry : IHeapObject {

    public PackageType packageType => PackageType.ActRegistry;

    public EffectId viewingProtectionEffectId; // m_viewingProtectionEID
    public Dictionary<ActId, List<SceneId>> actSceneTable; // m_actSceneTable

    public ActRegistry() {

    }

    public ActRegistry(AC2Reader data) {
        viewingProtectionEffectId = data.ReadEffectId();
        data.ReadHO<ARHash>(v => actSceneTable = v.to<ActId, List<SceneId>>(v => (v as AList).to<SceneId>()));
    }

    public void write(AC2Writer data) {
        data.Write(viewingProtectionEffectId);
        data.WriteHO(ARHash.from(actSceneTable, AList.from));
    }
}
