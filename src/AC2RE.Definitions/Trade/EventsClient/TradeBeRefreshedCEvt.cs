namespace AC2RE.Definitions;

public class TradeBeRefreshedCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeRefreshed;

    // WM_Trade::PostCEvt_Client_Trade_BeRefreshed
    public InstanceId sourceId; // _src

    public TradeBeRefreshedCEvt() {

    }

    public TradeBeRefreshedCEvt(AC2Reader data) {
        sourceId = data.UnpackInstanceId();
    }

    public void write(AC2Writer data) {
        data.Pack(sourceId);
    }
}
