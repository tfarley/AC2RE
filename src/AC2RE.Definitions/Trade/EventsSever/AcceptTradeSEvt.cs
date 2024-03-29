﻿namespace AC2RE.Definitions;

public class AcceptTradeSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__AcceptTrade;

    // WM_Trade::SendSEvt_AcceptTrade
    public Trade trade; // _stuff

    public AcceptTradeSEvt(AC2Reader data) {
        trade = data.UnpackHeapObject<Trade>();
    }
}
