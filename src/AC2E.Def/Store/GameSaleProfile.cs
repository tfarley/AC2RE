using System.IO;

namespace AC2E.Def {

    public class GameSaleProfile : SaleProfile {

        public override PackageType packageType => PackageType.GameSaleProfile;

        public uint m_uiOrdinal;
        public bool m_bRestricted;

        public GameSaleProfile() {

        }

        public GameSaleProfile(BinaryReader data, PackageRegistry registry) : base(data, registry) {
            m_uiOrdinal = data.ReadUInt32();
            m_bRestricted = data.ReadUInt32() != 0;
        }

        public override void write(BinaryWriter data, PackageRegistry registry) {
            base.write(data, registry);
            data.Write(m_uiOrdinal);
            data.Write(m_bRestricted ? (uint)1 : (uint)0);
        }
    }
}
