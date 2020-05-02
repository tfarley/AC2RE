using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using AC2E.Interp.Extensions;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Packages {

    public class InventProfilePkg : IPackage {

        public NativeType nativeType => NativeType.UNDEF;
        public PackageType packageType => PackageType.InventProfile;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

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
