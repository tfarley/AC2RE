namespace AC2E.Def {

    public class RequestNextSalesSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestNextSales;

        // WM_Store::SendSEvt_Store_RequestNextSales
        public InstanceId _iidStorekeeper;

        public RequestNextSalesSEvt(AC2Reader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
