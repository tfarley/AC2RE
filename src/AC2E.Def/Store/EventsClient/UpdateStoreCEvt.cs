using System.IO;

namespace AC2E.Def {

    public class UpdateStoreCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateStore;

        // WM_Store::PostCEvt_Store_UpdateStore
        public StoreView _view;
        public InstanceId _iidStorekeeper;

        public UpdateStoreCEvt() {

        }

        public UpdateStoreCEvt(BinaryReader data) {
            _view = data.UnpackPackage<StoreView>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_view);
            data.Pack(_iidStorekeeper);
        }
    }
}
