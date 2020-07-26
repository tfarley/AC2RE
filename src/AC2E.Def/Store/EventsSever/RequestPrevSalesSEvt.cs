namespace AC2E.Def {

    public class RequestPrevSalesSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestPrevSales;

        // WM_Store::SendSEvt_Store_RequestPrevSales
        public InstanceId storekeeperId; // _iidStorekeeper

        public RequestPrevSalesSEvt(AC2Reader data) {
            storekeeperId = data.UnpackInstanceId();
        }
    }
}
