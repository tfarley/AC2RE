using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class PlayerSaleProfilePkg : SaleProfilePkg {

        public override PackageType packageType => PackageType.PlayerSaleProfile;

        public int m_iQtyInStock;

        public PlayerSaleProfilePkg() {

        }

        public PlayerSaleProfilePkg(BinaryReader data, PackageRegistry registry) : base(data, registry) {
            m_iQtyInStock = data.ReadInt32();
        }

        public override void write(BinaryWriter data, PackageRegistry registry) {
            base.write(data, registry);
            data.Write(m_iQtyInStock);
        }
    }
}
