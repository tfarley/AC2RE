namespace AC2E.Def {

    public class TradeBeAcceptedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeAccepted;

        // WM_Trade::PostCEvt_Client_Trade_BeAccepted
        public InstanceId _src;

        public TradeBeAcceptedCEvt() {

        }

        public TradeBeAcceptedCEvt(AC2Reader data) {
            _src = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_src);
        }
    }
}
