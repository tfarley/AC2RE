using System.Collections.Generic;

namespace AC2RE.Definitions;

public class TransactInfo : IHeapObject {

    public PackageType packageType => PackageType.TransactInfo;

    public uint playerMoneyAdd; // m_uiPlayerMoneyAdd
    public bool hasLock; // m_bHasLock
    public Dictionary<InstanceId, uint> hashForSale; // m_hashForSale
    public Dictionary<InstanceId, uint> hashToBuy; // m_hashToBuy
    public uint playerMoneySubtract; // m_uiPlayerMoneySubtract
    public uint customerMoney; // m_uiCustomerMoney
    public List<ItemProfile> saleItemProfiles; // m_listSaleItemProfile
    public Dictionary<InstanceId, uint> hashToBuyErrors; // m_hashToBuyErrors
    public ErrorType error; // m_et
    public InstanceId customerId; // m_iidCustomer
    public Dictionary<InstanceId, uint> hashForSaleErrors; // m_hashForSaleErrors
    public InstanceId vendorId; // m_iidVendor

    public TransactInfo(AC2Reader data) {
        playerMoneyAdd = data.ReadUInt32();
        hasLock = data.ReadBoolean();
        data.ReadHO<LAHash>(v => hashForSale = v.to<InstanceId, uint>());
        data.ReadHO<LAHash>(v => hashToBuy = v.to<InstanceId, uint>());
        playerMoneySubtract = data.ReadUInt32();
        customerMoney = data.ReadUInt32();
        data.ReadHO<RList>(v => saleItemProfiles = v.to<ItemProfile>());
        data.ReadHO<LAHash>(v => hashToBuyErrors = v.to<InstanceId, uint>());
        error = (ErrorType)data.ReadUInt32();
        customerId = data.ReadInstanceId();
        data.ReadHO<LAHash>(v => hashForSaleErrors = v.to<InstanceId, uint>());
        vendorId = data.ReadInstanceId();
    }
}
