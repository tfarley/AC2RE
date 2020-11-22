namespace AC2RE.Definitions {

    public class LeaveCatalogCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__LeaveCatalog;

        // WM_Store::PostCEvt_Store_LeaveCatalog
        public InstanceId storekeeperId; // _iidStorekeeper

        public LeaveCatalogCEvt() {

        }

        public LeaveCatalogCEvt(AC2Reader data) {
            storekeeperId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(storekeeperId);
        }
    }
}
