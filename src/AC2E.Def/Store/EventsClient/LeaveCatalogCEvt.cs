using System.IO;

namespace AC2E.Def {

    public class LeaveCatalogCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__LeaveCatalog;

        // WM_Store::PostCEvt_Store_LeaveCatalog
        public InstanceId _iidStorekeeper;

        public LeaveCatalogCEvt() {

        }

        public LeaveCatalogCEvt(BinaryReader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iidStorekeeper);
        }
    }
}
