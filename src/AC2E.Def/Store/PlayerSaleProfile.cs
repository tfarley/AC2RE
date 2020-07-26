namespace AC2E.Def {

    public class PlayerSaleProfile : SaleProfile {

        public override PackageType packageType => PackageType.PlayerSaleProfile;

        public int quantityInStock; // m_iQtyInStock

        public PlayerSaleProfile() {

        }

        public PlayerSaleProfile(AC2Reader data) : base(data) {
            quantityInStock = data.ReadInt32();
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.Write(quantityInStock);
        }
    }
}
