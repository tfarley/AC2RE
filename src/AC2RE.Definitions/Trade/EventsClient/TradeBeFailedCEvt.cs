namespace AC2RE.Definitions;

public class TradeBeFailedCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeFailed;

    // WM_Trade::PostCEvt_Client_Trade_BeFailed
    public ErrorType error; // _etype

    public TradeBeFailedCEvt() {

    }

    public TradeBeFailedCEvt(AC2Reader data) {
        error = data.UnpackEnum<ErrorType>();
    }

    public void write(AC2Writer data) {
        data.PackEnum(error);
    }
}
