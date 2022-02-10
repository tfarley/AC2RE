namespace AC2RE.Definitions;

public class PatronSwearDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__Patron_SwearDone;

    // WM_Allegiance::PostCEvt_Patron_SwearDone
    public ErrorType error; // _etype
    public StringInfo vassalName; // _vassal_name
    public InstanceId vassalId; // _vassal

    public PatronSwearDoneCEvt() {

    }

    public PatronSwearDoneCEvt(AC2Reader data) {
        error = (ErrorType)data.UnpackUInt32();
        vassalName = data.UnpackHeapObject<StringInfo>();
        vassalId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack((uint)error);
        data.Pack(vassalName);
        data.Pack(vassalId);
    }
}
