namespace AC2E.Def {

    public class RequestSalesSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestSales;

        // WM_Store::SendSEvt_Store_RequestSales
        public uint _index;
        public InstanceId _iidStorekeeper;

        public RequestSalesSEvt(AC2Reader data) {
            _index = data.UnpackUInt32();
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
