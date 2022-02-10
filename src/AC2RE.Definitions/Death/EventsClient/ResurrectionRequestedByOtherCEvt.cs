namespace AC2RE.Definitions;

public class ResurrectionRequestedByOtherCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Death__ResurrectionRequestedByOther;

    // WM_Death::PostCEvt_ResurrectionRequestedByOther
    public ResurrectionRequest rezRequest; // _rezReq

    public ResurrectionRequestedByOtherCEvt() {

    }

    public ResurrectionRequestedByOtherCEvt(AC2Reader data) {
        rezRequest = data.UnpackHeapObject<ResurrectionRequest>();
    }

    public void write(AC2Writer data) {
        data.Pack(rezRequest);
    }
}
