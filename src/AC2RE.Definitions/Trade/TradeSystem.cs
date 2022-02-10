namespace AC2RE.Definitions;

public class TradeSystem : IHeapObject {

    public PackageType packageType => PackageType.TradeSystem;

    public InstanceId m_initialOfferItemId; // m_initialOfferItemID
    public uint initialOfferQuantity; // m_initialOfferQuantity

    public TradeSystem(AC2Reader data) {
        m_initialOfferItemId = data.ReadInstanceId();
        initialOfferQuantity = data.ReadUInt32();
    }
}
