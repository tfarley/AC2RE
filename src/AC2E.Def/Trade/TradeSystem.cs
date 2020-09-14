namespace AC2E.Def {

    public class TradeSystem : IPackage {

        public PackageType packageType => PackageType.TradeSystem;

        public InstanceId m_initialOfferItemId; // m_initialOfferItemID
        public uint initialOfferQuantity; // m_initialOfferQuantity

        public TradeSystem(AC2Reader data) {
            m_initialOfferItemId = data.ReadInstanceId();
            initialOfferQuantity = data.ReadUInt32();
        }
    }
}
