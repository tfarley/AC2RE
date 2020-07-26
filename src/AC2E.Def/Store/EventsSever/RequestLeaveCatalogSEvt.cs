namespace AC2E.Def {

    public class RequestLeaveCatalogSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestLeaveCatalog;

        // WM_Store::SendSEvt_Store_RequestLeaveCatalog
        public InstanceId storekeeperId; // _iidStorekeeper

        public RequestLeaveCatalogSEvt(AC2Reader data) {
            storekeeperId = data.UnpackInstanceId();
        }
    }
}
