using AC2E.Interp;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class VisualDescInfoPkg : IPackage {

        public PackageType packageType => PackageType.VisualDescInfo;

        public PkgRef<VectorPkg> m_scale;
        public PkgRef<AppInfoHashPkg> m_appInfoHash;
        public PkgRef<VisualDescPkg> m_cachedVisualDesc;

        public VisualDescInfoPkg() {

        }

        public VisualDescInfoPkg(BinaryReader data, List<Action<PackageRegistry>> resolvers) {
            m_scale = data.ReadPkgRef<VectorPkg>(resolvers);
            m_appInfoHash = data.ReadPkgRef<AppInfoHashPkg>(resolvers);
            m_cachedVisualDesc = data.ReadPkgRef<VisualDescPkg>(resolvers);
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(m_scale, references);
            data.Write(m_appInfoHash, references);
            data.Write(m_cachedVisualDesc, references);
        }
    }
}
