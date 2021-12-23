namespace AC2RE.Definitions;

public class UpdateSaleCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateSale;

    // WM_Store::PostCEvt_Store_UpdateSale
    public int stock; // __iStock
    public InstanceId itemId; // _iidItem
    public InstanceId storekeeperId; // _iidStorekeeper

    public UpdateSaleCEvt() {

    }

    public UpdateSaleCEvt(AC2Reader data) {
        stock = data.UnpackInt32();
        itemId = data.UnpackInstanceId();
        storekeeperId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(stock);
        data.Pack(itemId);
        data.Pack(storekeeperId);
    }
}
