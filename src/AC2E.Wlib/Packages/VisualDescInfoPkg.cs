using AC2E.Def;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class VisualDescInfoPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.VisualDescInfo;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public PkgRef<VectorPkg> m_scale;
        public PkgRef<AppInfoHashPkg> m_appInfoHash;
        public PkgRef<VisualDescPkg> m_cachedVisualDesc;

        public VisualDescInfoPkg() {

        }

        public VisualDescInfoPkg(BinaryReader data) {
            m_scale = data.ReadPkgRef<VectorPkg>();
            m_appInfoHash = data.ReadPkgRef<AppInfoHashPkg>();
            m_cachedVisualDesc = data.ReadPkgRef<VisualDescPkg>();
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(m_scale, references);
            data.Write(m_appInfoHash, references);
            data.Write(m_cachedVisualDesc, references);
        }
    }
}
