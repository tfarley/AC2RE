namespace AC2RE.Definitions;

public class UpdateFellowMaxHealthCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowMaxHealth;

    // WM_Fellowship::PostCEvt_UpdateFellowMaxHealth
    public uint maxHealth; // _value
    public InstanceId fellowId; // _fid

    public UpdateFellowMaxHealthCEvt() {

    }

    public UpdateFellowMaxHealthCEvt(AC2Reader data) {
        maxHealth = data.UnpackUInt32();
        fellowId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(maxHealth);
        data.Pack(fellowId);
    }
}
