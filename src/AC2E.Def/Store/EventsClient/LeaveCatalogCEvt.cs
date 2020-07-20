namespace AC2E.Def {

    public class LeaveCatalogCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__LeaveCatalog;

        // WM_Store::PostCEvt_Store_LeaveCatalog
        public InstanceId _iidStorekeeper;

        public LeaveCatalogCEvt() {

        }

        public LeaveCatalogCEvt(AC2Reader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_iidStorekeeper);
        }
    }
}
