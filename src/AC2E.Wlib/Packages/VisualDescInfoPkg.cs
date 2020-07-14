using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class VisualDescInfoPkg : IPackage {

        public PackageType packageType => PackageType.VisualDescInfo;

        public VectorPkg m_scale;
        public AppInfoHashPkg m_appInfoHash;
        public VisualDescPkg m_cachedVisualDesc;

        public VisualDescInfoPkg() {

        }

        public VisualDescInfoPkg(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<VectorPkg>(v => m_scale = v, registry);
            data.ReadPkgRef<AppInfoHashPkg>(v => m_appInfoHash = v, registry);
            data.ReadPkgRef<VisualDescPkg>(v => m_cachedVisualDesc = v, registry);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_scale, registry);
            data.Write(m_appInfoHash, registry);
            data.Write(m_cachedVisualDesc, registry);
        }
    }
}
