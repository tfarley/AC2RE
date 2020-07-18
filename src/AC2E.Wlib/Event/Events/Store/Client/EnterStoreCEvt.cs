using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class EnterStoreCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__EnterStore;

        // WM_Store::PostCEvt_Store_EnterStore
        public StoreViewPkg _view;
        public InstanceId _iidStorekeeper;

        public EnterStoreCEvt() {

        }

        public EnterStoreCEvt(BinaryReader data) {
            _view = data.UnpackPackage<StoreViewPkg>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_view);
            data.Pack(_iidStorekeeper);
        }
    }
}
