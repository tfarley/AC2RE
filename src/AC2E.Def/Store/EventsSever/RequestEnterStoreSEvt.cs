namespace AC2E.Def {

    public class RequestEnterStoreSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestEnterStore;

        // WM_Store::SendSEvt_Store_RequestEnterStore
        public DataId storeDid; // _didStore
        public InstanceId storekeeperId; // _iidStorekeeper

        public RequestEnterStoreSEvt(AC2Reader data) {
            storeDid = data.UnpackDataId();
            storekeeperId = data.UnpackInstanceId();
        }
    }
}
