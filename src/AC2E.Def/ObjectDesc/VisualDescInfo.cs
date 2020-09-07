using System.Numerics;

namespace AC2E.Def {

    public class VisualDescInfo : IPackage {

        public PackageType packageType => PackageType.VisualDescInfo;

        public Vector3 scale; // m_scale
        public AppInfoHash appInfoHash; // m_appInfoHash
        public VisualDesc cachedVisualDesc; // m_cachedVisualDesc

        public VisualDescInfo() {

        }

        public VisualDescInfo(AC2Reader data) {
            data.ReadPkg<VectorPkg>(v => scale = v.v);
            data.ReadPkg<AppInfoHash>(v => appInfoHash = v);
            data.ReadPkg<VisualDesc>(v => cachedVisualDesc = v);
        }

        public void write(AC2Writer data) {
            data.WritePkg(new VectorPkg(scale));
            data.WritePkg(appInfoHash);
            data.WritePkg(cachedVisualDesc);
        }
    }
}
