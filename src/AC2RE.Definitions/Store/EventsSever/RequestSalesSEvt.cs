namespace AC2RE.Definitions {

    public class RequestSalesSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestSales;

        // WM_Store::SendSEvt_Store_RequestSales
        public uint index; // _index
        public InstanceId storekeeperId; // _iidStorekeeper

        public RequestSalesSEvt(AC2Reader data) {
            index = data.UnpackUInt32();
            storekeeperId = data.UnpackInstanceId();
        }
    }
}
