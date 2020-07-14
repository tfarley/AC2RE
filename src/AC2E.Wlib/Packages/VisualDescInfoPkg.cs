using AC2E.Interp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class VisualDescInfoPkg : IPackage {

        public PackageType packageType => PackageType.VisualDescInfo;

        public VectorPkg m_scale;
        public AppInfoHashPkg m_appInfoHash;
        public VisualDescPkg m_cachedVisualDesc;

        public VisualDescInfoPkg() {

        }

        public VisualDescInfoPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            data.ReadPkgRef<VectorPkg>(v => m_scale = v, resolvers);
            data.ReadPkgRef<AppInfoHashPkg>(v => m_appInfoHash = v, resolvers);
            data.ReadPkgRef<VisualDescPkg>(v => m_cachedVisualDesc = v, resolvers);
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_scale, references);
            data.Write(m_appInfoHash, references);
            data.Write(m_cachedVisualDesc, references);
        }
    }
}
