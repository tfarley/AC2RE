namespace AC2E.Def {

    public class InitSaleRemindersCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Store__InitSaleReminders;

        // WM_Store::PostCEvt_Store_InitSaleReminders
        public ConsignerDesc _desc;
        public InstanceId _iidStorekeeper;

        public InitSaleRemindersCEvt() {

        }

        public InitSaleRemindersCEvt(AC2Reader data) {
            _desc = data.UnpackPackage<ConsignerDesc>();
            _iidStorekeeper = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_desc);
            data.Pack(_iidStorekeeper);
        }
    }
}
