namespace AC2E.Def {

    public class PlayerSaleProfile : SaleProfile {

        public override PackageType packageType => PackageType.PlayerSaleProfile;

        public int m_iQtyInStock;

        public PlayerSaleProfile() {

        }

        public PlayerSaleProfile(AC2Reader data, PackageRegistry registry) : base(data, registry) {
            m_iQtyInStock = data.ReadInt32();
        }

        public override void write(AC2Writer data, PackageRegistry registry) {
            base.write(data, registry);
            data.Write(m_iQtyInStock);
        }
    }
}
