using System.IO;

namespace AC2E.Def {

    public class Consignment : IPackage {

        public PackageType packageType => PackageType.Consignment;

        public PlayerSaleProfile m_profile;
        public InstanceId m_iidOwner;
        public uint m_saleID;
        public int m_quantityOffered;
        public int m_quantitySold;
        public double m_ttTimeEntered;
        public uint m_uiFlags;

        public Consignment() {

        }

        public Consignment(BinaryReader data, PackageRegistry registry) {
            data.ReadPkgRef<PlayerSaleProfile>(v => m_profile = v, registry);
            m_iidOwner = data.ReadInstanceId();
            m_saleID = data.ReadUInt32();
            m_quantityOffered = data.ReadInt32();
            m_quantitySold = data.ReadInt32();
            m_ttTimeEntered = data.ReadDouble();
            m_uiFlags = data.ReadUInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_profile, registry);
            data.Write(m_iidOwner);
            data.Write(m_saleID);
            data.Write(m_quantityOffered);
            data.Write(m_quantitySold);
            data.Write(m_ttTimeEntered);
            data.Write(m_uiFlags);
        }
    }
}
