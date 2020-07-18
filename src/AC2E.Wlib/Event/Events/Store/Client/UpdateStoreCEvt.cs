using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdateStoreCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__UpdateStore;

        // WM_Store::PostCEvt_Store_UpdateStore
        public StoreViewPkg _view;
        public InstanceId _iidStorekeeper;

        public UpdateStoreCEvt() {

        }

        public UpdateStoreCEvt(BinaryReader data) {
            _view = data.UnpackPackage<StoreViewPkg>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_view);
            data.Pack(_iidStorekeeper);
        }
    }
}
