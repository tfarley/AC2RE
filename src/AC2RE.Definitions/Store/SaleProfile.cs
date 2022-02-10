namespace AC2RE.Definitions;

public class SaleProfile : IHeapObject {

    public virtual PackageType packageType => PackageType.SaleProfile;

    public DataId productDid; // m_didProduct
    public IconDesc productIconDesc; // m_productIconDesc
    public InstanceId productId; // m_iidProduct
    public DataId productIconDid; // m_productIconDID
    public StringInfo productName; // m_siProductName
    public float cost; // m_fCost
    public DataId tradeDid; // m_didTrade
    public int maxStackSize; // m_iMaxStackSize

    public SaleProfile() {

    }

    public SaleProfile(AC2Reader data) {
        productDid = data.ReadDataId();
        data.ReadHO<IconDesc>(v => productIconDesc = v);
        productId = data.ReadInstanceId();
        productIconDid = data.ReadDataId();
        data.ReadHO<StringInfo>(v => productName = v);
        cost = data.ReadSingle();
        tradeDid = data.ReadDataId();
        maxStackSize = data.ReadInt32();
    }

    public virtual void write(AC2Writer data) {
        data.Write(productDid);
        data.WriteHO(productIconDesc);
        data.Write(productId);
        data.Write(productIconDid);
        data.WriteHO(productName);
        data.Write(cost);
        data.Write(tradeDid);
        data.Write(maxStackSize);
    }
}
