﻿namespace AC2RE.Definitions;

public class ClearFellowCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__ClearFellow;

    // WM_Fellowship::PostCEvt_ClearFellow
    public InstanceId fellowId; // _fid

    public ClearFellowCEvt() {

    }

    public ClearFellowCEvt(AC2Reader data) {
        fellowId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(fellowId);
    }
}
