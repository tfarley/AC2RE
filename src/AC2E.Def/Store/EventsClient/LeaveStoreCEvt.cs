using System.IO;

namespace AC2E.Def {

    public class LeaveStoreCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__LeaveStore;

        // WM_Store::PostCEvt_Store_LeaveStore
        public InstanceId _iidStorekeeper;

        public LeaveStoreCEvt() {

        }

        public LeaveStoreCEvt(BinaryReader data) {
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iidStorekeeper);
        }
    }
}
