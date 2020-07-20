namespace AC2E.Def {

    public class TransactionBlob : IPackage {

        public PackageType packageType => PackageType.TransactionBlob;

        public InstanceId m_iidItem;
        public InstanceIdList m_tradeItems;
        public InstanceId m_iidShopper;
        public uint m_uiQuantity;
        public InstanceId m_iidStorekeeper;
        public uint m_result;
        public uint m_uiSlot;

        public TransactionBlob() {

        }

        public TransactionBlob(AC2Reader data) {
            m_iidItem = data.ReadInstanceId();
            data.ReadPkg<LList>(v => m_tradeItems = new InstanceIdList(v));
            m_iidShopper = data.ReadInstanceId();
            m_uiQuantity = data.ReadUInt32();
            m_iidStorekeeper = data.ReadInstanceId();
            m_result = data.ReadUInt32();
            m_uiSlot = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(m_iidItem);
            data.WritePkg(m_tradeItems);
            data.Write(m_iidShopper);
            data.Write(m_uiQuantity);
            data.Write(m_iidStorekeeper);
            data.Write(m_result);
            data.Write(m_uiSlot);
        }
    }
}
