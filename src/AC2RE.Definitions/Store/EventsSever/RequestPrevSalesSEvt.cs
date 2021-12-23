namespace AC2RE.Definitions;

public class RequestPrevSalesSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestPrevSales;

    // WM_Store::SendSEvt_Store_RequestPrevSales
    public InstanceId storekeeperId; // _iidStorekeeper

    public RequestPrevSalesSEvt(AC2Reader data) {
        storekeeperId = data.UnpackInstanceId();
    }
}
