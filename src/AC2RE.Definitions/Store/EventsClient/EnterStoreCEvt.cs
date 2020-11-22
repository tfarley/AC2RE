namespace AC2RE.Definitions {

    public class EnterStoreCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterStore;

        // WM_Store::PostCEvt_Store_EnterStore
        public StoreView view; // _view
        public InstanceId storekeeperId; // _iidStorekeeper

        public EnterStoreCEvt() {

        }

        public EnterStoreCEvt(AC2Reader data) {
            view = data.UnpackPackage<StoreView>();
            storekeeperId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(view);
            data.Pack(storekeeperId);
        }
    }
}
