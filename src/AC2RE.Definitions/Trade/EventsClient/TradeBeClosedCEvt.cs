namespace AC2RE.Definitions;

public class TradeBeClosedCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeClosed;

    // WM_Trade::PostCEvt_Client_Trade_BeClosed
    public ErrorType error; // _etype
    public InstanceId sourceId; // _src

    public TradeBeClosedCEvt() {

    }

    public TradeBeClosedCEvt(AC2Reader data) {
        error = data.UnpackEnum<ErrorType>();
        sourceId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.PackEnum(error);
        data.Pack(sourceId);
    }
}
