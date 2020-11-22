using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ActRegistry : IPackage {

        public PackageType packageType => PackageType.ActRegistry;

        public int viewingProtectionEffectId; // m_viewingProtectionEID
        public Dictionary<uint, List<uint>> actSceneTable; // m_actSceneTable

        public ActRegistry() {

        }

        public ActRegistry(AC2Reader data) {
            viewingProtectionEffectId = data.ReadInt32();
            data.ReadPkg<ARHash>(v => actSceneTable = v.to<uint, List<uint>>());
        }

        public void write(AC2Writer data) {
            data.Write(viewingProtectionEffectId);
            data.WritePkg(ARHash.from(actSceneTable, AList.from));
        }
    }
}
