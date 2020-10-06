namespace AC2E.Def {

    public class TradeBeClosedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeClosed;

        // WM_Trade::PostCEvt_Client_Trade_BeClosed
        public ErrorType error; // _etype
        public InstanceId sourceId; // _src

        public TradeBeClosedCEvt() {

        }

        public TradeBeClosedCEvt(AC2Reader data) {
            error = (ErrorType)data.UnpackUInt32();
            sourceId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack((uint)error);
            data.Pack(sourceId);
        }
    }
}
