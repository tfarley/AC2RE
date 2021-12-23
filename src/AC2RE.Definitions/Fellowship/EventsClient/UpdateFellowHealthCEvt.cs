namespace AC2RE.Definitions;

public class UpdateFellowHealthCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowHealth;

    // WM_Fellowship::PostCEvt_UpdateFellowHealth
    public uint healthPk; // _valuePK
    public uint health; // _value
    public InstanceId fellowId; // _fid

    public UpdateFellowHealthCEvt() {

    }

    public UpdateFellowHealthCEvt(AC2Reader data) {
        healthPk = data.UnpackUInt32();
        health = data.UnpackUInt32();
        fellowId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(healthPk);
        data.Pack(health);
        data.Pack(fellowId);
    }
}
