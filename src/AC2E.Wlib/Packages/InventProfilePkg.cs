using AC2E.Def;
using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class InventProfilePkg : IPackage {

        public PackageType packageType => PackageType.InventProfile;

        public VisualDescInfoPkg m_visualDescInfo;
        public uint m_slotsTaken;
        public uint m_location;
        public int m_it;
        public InstanceId m_iid;

        public InventProfilePkg() {

        }

        public InventProfilePkg(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<VisualDescInfoPkg>(v => m_visualDescInfo = v, registry);
            m_slotsTaken = data.ReadUInt32();
            m_location = data.ReadUInt32();
            m_it = data.ReadInt32();
            m_iid = data.ReadInstanceId();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_visualDescInfo, registry);
            data.Write(m_slotsTaken);
            data.Write(m_location);
            data.Write(m_it);
            data.Write(m_iid);
        }
    }
}
