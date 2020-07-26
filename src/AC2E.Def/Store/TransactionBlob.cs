namespace AC2E.Def {

    public class TransactionBlob : IPackage {

        public PackageType packageType => PackageType.TransactionBlob;

        public InstanceId itemId; // m_iidItem
        public InstanceIdList tradeItemIds; // m_tradeItems
        public InstanceId shopperId; // m_iidShopper
        public uint quantity; // m_uiQuantity
        public InstanceId storekeeperId; // m_iidStorekeeper
        public uint result; // m_result
        public uint slot; // m_uiSlot

        public TransactionBlob() {

        }

        public TransactionBlob(AC2Reader data) {
            itemId = data.ReadInstanceId();
            data.ReadPkg<LList>(v => tradeItemIds = new InstanceIdList(v));
            shopperId = data.ReadInstanceId();
            quantity = data.ReadUInt32();
            storekeeperId = data.ReadInstanceId();
            result = data.ReadUInt32();
            slot = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(itemId);
            data.WritePkg(tradeItemIds);
            data.Write(shopperId);
            data.Write(quantity);
            data.Write(storekeeperId);
            data.Write(result);
            data.Write(slot);
        }
    }
}
