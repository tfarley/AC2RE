namespace AC2E.Def {

    public class RequestLeaveStoreSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestLeaveStore;

        // WM_Store::SendSEvt_Store_RequestLeaveStore
        public InstanceId storekeeperId; // _iidStorekeeper

        public RequestLeaveStoreSEvt(AC2Reader data) {
            storekeeperId = data.UnpackInstanceId();
        }
    }
}
