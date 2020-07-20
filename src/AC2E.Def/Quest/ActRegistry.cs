namespace AC2E.Def {

    public class ActRegistry : IPackage {

        public PackageType packageType => PackageType.ActRegistry;

        public int m_viewingProtectionEID;
        public ARHash<AList> m_actSceneTable;

        public ActRegistry() {

        }

        public ActRegistry(AC2Reader data) {
            m_viewingProtectionEID = data.ReadInt32();
            data.ReadPkg<ARHash<IPackage>>(v => m_actSceneTable = v.to<AList>());
        }

        public void write(AC2Writer data) {
            data.Write(m_viewingProtectionEID);
            data.WritePkg(m_actSceneTable);
        }
    }
}
