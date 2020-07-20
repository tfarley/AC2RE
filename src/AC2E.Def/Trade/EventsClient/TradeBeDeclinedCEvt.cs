namespace AC2E.Def {

    public class TradeBeDeclinedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeDeclined;

        // WM_Trade::PostCEvt_Client_Trade_BeDeclined
        public InstanceId _src;

        public TradeBeDeclinedCEvt() {

        }

        public TradeBeDeclinedCEvt(AC2Reader data) {
            _src = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_src);
        }
    }
}
