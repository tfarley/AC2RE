namespace AC2E.Def {

    public class PlayerSaleProfile : SaleProfile {

        public override PackageType packageType => PackageType.PlayerSaleProfile;

        public int m_iQtyInStock;

        public PlayerSaleProfile() {

        }

        public PlayerSaleProfile(AC2Reader data) : base(data) {
            m_iQtyInStock = data.ReadInt32();
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.Write(m_iQtyInStock);
        }
    }
}
