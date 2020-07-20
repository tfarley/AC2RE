namespace AC2E.Def {

    public class RequestCollectSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestCollect;

        // WM_Store::SendSEvt_Store_RequestCollect
        public bool _bProfits;
        public uint _target;
        public InstanceId _iidStorekeeper;

        public RequestCollectSEvt(AC2Reader data) {
            _bProfits = data.UnpackBoolean();
            _target = data.UnpackUInt32();
            _iidStorekeeper = data.UnpackInstanceId();
        }
    }
}
