using System.IO;

namespace AC2E.Def {

    public class InitSaleRemindersCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__InitSaleReminders;

        // WM_Store::PostCEvt_Store_InitSaleReminders
        public ConsignerDesc _desc;
        public InstanceId _iidStorekeeper;

        public InitSaleRemindersCEvt() {

        }

        public InitSaleRemindersCEvt(BinaryReader data) {
            _desc = data.UnpackPackage<ConsignerDesc>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_desc);
            data.Pack(_iidStorekeeper);
        }
    }
}
