namespace AC2E.Def {

    public class VisualDescInfo : IPackage {

        public PackageType packageType => PackageType.VisualDescInfo;

        public Vector m_scale;
        public AppInfoHash m_appInfoHash;
        public VisualDesc m_cachedVisualDesc;

        public VisualDescInfo() {

        }

        public VisualDescInfo(AC2Reader data, PackageRegistry registry) {
            data.ReadPkgRef<Vector>(v => m_scale = v, registry);
            data.ReadPkgRef<AppInfoHash>(v => m_appInfoHash = v, registry);
            data.ReadPkgRef<VisualDesc>(v => m_cachedVisualDesc = v, registry);
        }

        public void write(AC2Writer data, PackageRegistry registry) {
            data.Write(m_scale, registry);
            data.Write(m_appInfoHash, registry);
            data.Write(m_cachedVisualDesc, registry);
        }
    }
}
