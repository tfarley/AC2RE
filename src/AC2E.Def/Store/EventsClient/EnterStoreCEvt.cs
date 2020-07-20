namespace AC2E.Def {

    public class EnterStoreCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterStore;

        // WM_Store::PostCEvt_Store_EnterStore
        public StoreView _view;
        public InstanceId _iidStorekeeper;

        public EnterStoreCEvt() {

        }

        public EnterStoreCEvt(AC2Reader data) {
            _view = data.UnpackPackage<StoreView>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_view);
            data.Pack(_iidStorekeeper);
        }
    }
}
