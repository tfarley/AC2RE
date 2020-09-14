namespace AC2E.Def {

    public class TransactInfo : IPackage {

        public PackageType packageType => PackageType.TransactInfo;

        public uint playerMoneyAdd; // m_uiPlayerMoneyAdd
        public bool hasLock; // m_bHasLock
        public InstanceIdAHash hashForSale; // m_hashForSale
        public InstanceIdAHash hashToBuy; // m_hashToBuy
        public uint playerMoneySubtract; // m_uiPlayerMoneySubtract
        public uint customerMoney; // m_uiCustomerMoney
        public RList<ItemProfile> saleItemProfiles; // m_listSaleItemProfile
        public InstanceIdAHash hashToBuyErrors; // m_hashToBuyErrors
        public uint errorType; // m_et
        public InstanceId customerId; // m_iidCustomer
        public InstanceIdAHash hashForSaleErrors; // m_hashForSaleErrors
        public InstanceId vendorId; // m_iidVendor

        public TransactInfo(AC2Reader data) {
            playerMoneyAdd = data.ReadUInt32();
            hasLock = data.ReadBoolean();
            data.ReadPkg<LAHash>(v => hashForSale = new InstanceIdAHash(v));
            data.ReadPkg<LAHash>(v => hashToBuy = new InstanceIdAHash(v));
            playerMoneySubtract = data.ReadUInt32();
            customerMoney = data.ReadUInt32();
            data.ReadPkg<RList<ItemProfile>>(v => saleItemProfiles = v.to<ItemProfile>());
            data.ReadPkg<LAHash>(v => hashToBuyErrors = new InstanceIdAHash(v));
            errorType = data.ReadUInt32();
            customerId = data.ReadInstanceId();
            data.ReadPkg<LAHash>(v => hashForSaleErrors = new InstanceIdAHash(v));
            vendorId = data.ReadInstanceId();
        }
    }
}
