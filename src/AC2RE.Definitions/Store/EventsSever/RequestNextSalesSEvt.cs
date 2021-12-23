namespace AC2RE.Definitions;

public class RequestNextSalesSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestNextSales;

    // WM_Store::SendSEvt_Store_RequestNextSales
    public InstanceId storekeeperId; // _iidStorekeeper

    public RequestNextSalesSEvt(AC2Reader data) {
        storekeeperId = data.UnpackInstanceId();
    }
}
