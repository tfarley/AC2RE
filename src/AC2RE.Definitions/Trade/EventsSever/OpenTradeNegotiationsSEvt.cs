namespace AC2RE.Definitions;

public class OpenTradeNegotiationsSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Trade__OpenTradeNegotiations;

    // WM_Trade::SendSEvt_OpenTradeNegotiations
    public InstanceId targetId; // _trg

    public OpenTradeNegotiationsSEvt(AC2Reader data) {
        targetId = data.UnpackInstanceId();
    }
}
