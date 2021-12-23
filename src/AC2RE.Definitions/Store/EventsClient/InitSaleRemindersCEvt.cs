namespace AC2RE.Definitions;

public class InitSaleRemindersCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Store__InitSaleReminders;

    // WM_Store::PostCEvt_Store_InitSaleReminders
    public ConsignerDesc consignerDesc; // _desc
    public InstanceId storekeeperId; // _iidStorekeeper

    public InitSaleRemindersCEvt() {

    }

    public InitSaleRemindersCEvt(AC2Reader data) {
        consignerDesc = data.UnpackPackage<ConsignerDesc>();
        storekeeperId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(consignerDesc);
        data.Pack(storekeeperId);
    }
}
