using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ActRegistry : IPackage {

        public PackageType packageType => PackageType.ActRegistry;

        public EffectId viewingProtectionEffectId; // m_viewingProtectionEID
        public Dictionary<uint, List<SceneId>> actSceneTable; // m_actSceneTable

        public ActRegistry() {

        }

        public ActRegistry(AC2Reader data) {
            viewingProtectionEffectId = data.ReadEffectId();
            data.ReadPkg<ARHash>(v => actSceneTable = v.to<uint, List<SceneId>>(v => (v as AList).to<SceneId>()));
        }

        public void write(AC2Writer data) {
            data.Write(viewingProtectionEffectId);
            data.WritePkg(ARHash.from(actSceneTable, AList.from));
        }
    }
}
