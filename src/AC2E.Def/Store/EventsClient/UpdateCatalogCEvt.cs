namespace AC2E.Def {

    public class UpdateCatalogCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateCatalog;

        // WM_Store::PostCEvt_Store_UpdateCatalog
        public AAHash _view;
        public DataId _didCatalog;
        public InstanceId _iidStorekeeper;

        public UpdateCatalogCEvt() {

        }

        public UpdateCatalogCEvt(AC2Reader data) {
            _view = data.UnpackPackage<AAHash>();
            _didCatalog = data.UnpackDataId();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_view);
            data.Pack(_didCatalog);
            data.Pack(_iidStorekeeper);
        }
    }
}
