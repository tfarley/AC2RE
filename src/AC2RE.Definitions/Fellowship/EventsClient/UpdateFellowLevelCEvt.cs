namespace AC2RE.Definitions;

public class UpdateFellowLevelCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowLevel;

    // WM_Fellowship::PostCEvt_UpdateFellowLevel
    public uint level; // _new_level
    public InstanceId fellowId; // _fid

    public UpdateFellowLevelCEvt() {

    }

    public UpdateFellowLevelCEvt(AC2Reader data) {
        level = data.UnpackUInt32();
        fellowId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(level);
        data.Pack(fellowId);
    }
}
