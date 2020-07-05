using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class ActRegistryPkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.ActRegistry;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public int m_viewingProtectionEID;
        public ARHashPkg<AListPkg> m_actSceneTable;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_viewingProtectionEID);
            data.Write(m_actSceneTable, references);
        }
    }
}
