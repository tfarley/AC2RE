using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class VisualDescInfoPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.VisualDescInfo;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public VectorPkg m_scale;
        public AppInfoHashPkg m_appInfoHash;
        public VisualDescPkg m_cachedVisualDesc;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_scale, references);
            data.Write(m_appInfoHash, references);
            data.Write(m_cachedVisualDesc, references);
        }
    }
}
