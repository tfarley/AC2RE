﻿namespace AC2RE.Definitions;

public class BasicAttacksFailedCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__BasicAttacksFailed;

    // WM_Combat::PostCEvt_Combat_BasicAttacksFailed
    public ErrorType error; // _err

    public BasicAttacksFailedCEvt() {

    }

    public BasicAttacksFailedCEvt(AC2Reader data) {
        error = data.UnpackEnum<ErrorType>();
    }

    public void write(AC2Writer data) {
        data.PackEnum(error);
    }
}
