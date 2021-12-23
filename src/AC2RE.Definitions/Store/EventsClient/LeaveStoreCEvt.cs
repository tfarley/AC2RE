namespace AC2RE.Definitions;

public class LeaveStoreCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Store__LeaveStore;

    // WM_Store::PostCEvt_Store_LeaveStore
    public InstanceId storekeeperId; // _iidStorekeeper

    public LeaveStoreCEvt() {

    }

    public LeaveStoreCEvt(AC2Reader data) {
        storekeeperId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(storekeeperId);
    }
}
