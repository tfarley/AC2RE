using AC2E.Def;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class InventProfilePkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.InventProfile;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public PackageId id { get; set; }

        public VisualDescInfoPkg m_visualDescInfo;
        public uint m_slotsTaken;
        public uint m_location;
        public int m_it;
        public InstanceId m_iid;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_visualDescInfo, references);
            data.Write(m_slotsTaken);
            data.Write(m_location);
            data.Write(m_it);
            data.Write(m_iid);
        }
    }
}
