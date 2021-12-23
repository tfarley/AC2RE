namespace AC2RE.Definitions;

public class UpdateFellowCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellow;

    // WM_Fellowship::PostCEvt_UpdateFellow
    public Fellow fellow; // _fellow
    public InstanceId fellowId; // _fid

    public UpdateFellowCEvt() {

    }

    public UpdateFellowCEvt(AC2Reader data) {
        fellow = data.UnpackPackage<Fellow>();
        fellowId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(fellow);
        data.Pack(fellowId);
    }
}
