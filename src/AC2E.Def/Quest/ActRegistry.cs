namespace AC2E.Def {

    public class ActRegistry : IPackage {

        public PackageType packageType => PackageType.ActRegistry;

        public int viewingProtectionEffectId; // m_viewingProtectionEID
        public ARHash<AList> actSceneTable; // m_actSceneTable

        public ActRegistry() {

        }

        public ActRegistry(AC2Reader data) {
            viewingProtectionEffectId = data.ReadInt32();
            data.ReadPkg<ARHash<IPackage>>(v => actSceneTable = v.to<AList>());
        }

        public void write(AC2Writer data) {
            data.Write(viewingProtectionEffectId);
            data.WritePkg(actSceneTable);
        }
    }
}
