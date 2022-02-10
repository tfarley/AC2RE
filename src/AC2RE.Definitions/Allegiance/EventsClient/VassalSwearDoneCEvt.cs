namespace AC2RE.Definitions;

public class VassalSwearDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__Vassal_SwearDone;

    // WM_Allegiance::PostCEvt_Vassal_SwearDone
    public ErrorType error; // _etype
    public StringInfo patronName; // _patron_name
    public InstanceId patronId; // _patron

    public VassalSwearDoneCEvt() {

    }

    public VassalSwearDoneCEvt(AC2Reader data) {
        error = (ErrorType)data.UnpackUInt32();
        patronName = data.UnpackHeapObject<StringInfo>();
        patronId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack((uint)error);
        data.Pack(patronName);
        data.Pack(patronId);
    }
}
