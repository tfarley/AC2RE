using System.Collections.Generic;

namespace AC2RE.Definitions;

public class TransactionBlob : IHeapObject {

    public PackageType packageType => PackageType.TransactionBlob;

    public InstanceId itemId; // m_iidItem
    public List<InstanceId> tradeItemIds; // m_tradeItems
    public InstanceId shopperId; // m_iidShopper
    public uint quantity; // m_uiQuantity
    public InstanceId storekeeperId; // m_iidStorekeeper
    public ErrorType error; // m_result
    public uint slot; // m_uiSlot

    public TransactionBlob() {

    }

    public TransactionBlob(AC2Reader data) {
        itemId = data.ReadInstanceId();
        data.ReadHO<LList>(v => tradeItemIds = v.to<InstanceId>());
        shopperId = data.ReadInstanceId();
        quantity = data.ReadUInt32();
        storekeeperId = data.ReadInstanceId();
        error = (ErrorType)data.ReadUInt32();
        slot = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        data.Write(itemId);
        data.WriteHO(LList.from(tradeItemIds));
        data.Write(shopperId);
        data.Write(quantity);
        data.Write(storekeeperId);
        data.Write((uint)error);
        data.Write(slot);
    }
}
