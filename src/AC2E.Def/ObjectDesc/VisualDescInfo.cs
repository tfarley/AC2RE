namespace AC2E.Def {

    public class VisualDescInfo : IPackage {

        public PackageType packageType => PackageType.VisualDescInfo;

        public Vector scale; // m_scale
        public AppInfoHash appInfoHash; // m_appInfoHash
        public VisualDesc cachedVisualDesc; // m_cachedVisualDesc

        public VisualDescInfo() {

        }

        public VisualDescInfo(AC2Reader data) {
            data.ReadPkg<Vector>(v => scale = v);
            data.ReadPkg<AppInfoHash>(v => appInfoHash = v);
            data.ReadPkg<VisualDesc>(v => cachedVisualDesc = v);
        }

        public void write(AC2Writer data) {
            data.WritePkg(scale);
            data.WritePkg(appInfoHash);
            data.WritePkg(cachedVisualDesc);
        }
    }
}
