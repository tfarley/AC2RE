namespace AC2E.Def {

    public class RequestEnterStoreSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestEnterStore;

        // WM_Store::SendSEvt_Store_RequestEnterStore
        public DataId _didStore;
        public InstanceId _iidStorekeeper;

        public RequestEnterStoreSEvt(AC2Reader data) {
            _didStore = data.UnpackDataId();
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
