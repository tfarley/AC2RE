namespace AC2E.Def {

    public class ActRegistry : IPackage {

        public PackageType packageType => PackageType.ActRegistry;

        public int m_viewingProtectionEID;
        public ARHash<AList> m_actSceneTable;

        public ActRegistry() {

        }

        public ActRegistry(AC2Reader data, PackageRegistry registry) {
            m_viewingProtectionEID = data.ReadInt32();
            data.ReadPkgRef<ARHash<IPackage>>(v => m_actSceneTable = v.to<AList>(), registry);
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_viewingProtectionEID);
            data.Write(m_actSceneTable, registry);
        }
    }
}
