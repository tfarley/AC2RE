namespace AC2E.Def {

    public class VisualDescInfo : IPackage {

        public PackageType packageType => PackageType.VisualDescInfo;

        public Vector m_scale;
        public AppInfoHash m_appInfoHash;
        public VisualDesc m_cachedVisualDesc;

        public VisualDescInfo() {

        }

        public VisualDescInfo(AC2Reader data) {
            data.ReadPkg<Vector>(v => m_scale = v);
            data.ReadPkg<AppInfoHash>(v => m_appInfoHash = v);
            data.ReadPkg<VisualDesc>(v => m_cachedVisualDesc = v);
        }

        public void write(AC2Writer data) {
            data.WritePkg(m_scale);
            data.WritePkg(m_appInfoHash);
            data.WritePkg(m_cachedVisualDesc);
        }
    }
}
