namespace AC2E.Def {

    public class UpdateCatalogCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateCatalog;

        // WM_Store::PostCEvt_Store_UpdateCatalog
        public AAHash view; // _view
        public DataId catalogDid; // _didCatalog
        public InstanceId storekeeperId; // _iidStorekeeper

        public UpdateCatalogCEvt() {

        }

        public UpdateCatalogCEvt(AC2Reader data) {
            view = data.UnpackPackage<AAHash>();
            catalogDid = data.UnpackDataId();
            storekeeperId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(view);
            data.Pack(catalogDid);
            data.Pack(storekeeperId);
        }
    }
}
