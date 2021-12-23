namespace AC2RE.Definitions;

public class StoreRequestDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Store__Request_Done;

    // WM_Store::PostCEvt_Store_Request_Done
    public ErrorType error; // _err

    public StoreRequestDoneCEvt() {

    }

    public StoreRequestDoneCEvt(AC2Reader data) {
        error = (ErrorType)data.UnpackUInt32();
    }

    public void write(AC2Writer data) {
        data.Pack((uint)error);
    }
}
