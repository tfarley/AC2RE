using System.IO;

namespace AC2E.Def {

    public class InventProfile : IPackage {

        public PackageType packageType => PackageType.InventProfile;

        public VisualDescInfo m_visualDescInfo;
        public uint m_slotsTaken;
        public uint m_location;
        public int m_it;
        public InstanceId m_iid;

        public InventProfile() {

        }

        public InventProfile(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<VisualDescInfo>(v => m_visualDescInfo = v, registry);
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
