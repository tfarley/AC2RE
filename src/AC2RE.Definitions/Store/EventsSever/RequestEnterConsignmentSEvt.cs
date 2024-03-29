﻿namespace AC2RE.Definitions;

public class RequestEnterConsignmentSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Store__RequestEnterConsignment;

    // WM_Store::SendSEvt_Store_RequestEnterConsignment
    public InstanceId storekeeperId; // _iidStorekeeper

    public RequestEnterConsignmentSEvt(AC2Reader data) {
        storekeeperId = data.UnpackInstanceId();
    }
}
