namespace AC2RE.Definitions {

    public class TradeBeFailedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeFailed;

        // WM_Trade::PostCEvt_Client_Trade_BeFailed
        public ErrorType error; // _etype

        public TradeBeFailedCEvt() {

        }

        public TradeBeFailedCEvt(AC2Reader data) {
            error = (ErrorType)data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack((uint)error);
        }
    }
}
