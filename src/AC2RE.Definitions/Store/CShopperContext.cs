namespace AC2RE.Definitions;

public class CShopperContext : IHeapObject {

    public PackageType packageType => PackageType.CShopperContext;

    public DataId catalogDid; // m_didCatalog
    public StoreView view; // m_view
    public InstanceId storekeeperId; // m_iidStorekeeper
    public TransactionBlob pendingTransaction; // m_pendingTransaction

    public CShopperContext(AC2Reader data) {
        catalogDid = data.ReadDataId();
        data.ReadHO<StoreView>(v => view = v);
        storekeeperId = data.ReadInstanceId();
        data.ReadHO<TransactionBlob>(v => pendingTransaction = v);
    }
}
