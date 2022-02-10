namespace AC2RE.Definitions;

public class PurchaseItemFromStoreSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Store__PurchaseItemFromStore;

    // WM_Store::SendSEvt_Store_PurchaseItemFromStore
    public TransactionBlob transactionBlob; // _blob

    public PurchaseItemFromStoreSEvt(AC2Reader data) {
        transactionBlob = data.UnpackHeapObject<TransactionBlob>();
    }
}
