namespace AC2RE.Definitions;

public class UpdateFellowshipCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowship;

    // WM_Fellowship::PostCEvt_UpdateFellowship
    public Fellowship fellowship; // _fellowship

    public UpdateFellowshipCEvt() {

    }

    public UpdateFellowshipCEvt(AC2Reader data) {
        fellowship = data.UnpackHeapObject<Fellowship>();
    }

    public void write(AC2Writer data) {
        data.Pack(fellowship);
    }
}
