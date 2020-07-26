namespace AC2E.Def {

    public class TradeBeFailedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeFailed;

        // WM_Trade::PostCEvt_Client_Trade_BeFailed
        public uint error; // _etype

        public TradeBeFailedCEvt() {

        }

        public TradeBeFailedCEvt(AC2Reader data) {
            error = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(error);
        }
    }
}
