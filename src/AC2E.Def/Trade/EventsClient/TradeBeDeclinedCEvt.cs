namespace AC2E.Def {

    public class TradeBeDeclinedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeDeclined;

        // WM_Trade::PostCEvt_Client_Trade_BeDeclined
        public InstanceId sourceId; // _src

        public TradeBeDeclinedCEvt() {

        }

        public TradeBeDeclinedCEvt(AC2Reader data) {
            sourceId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(sourceId);
        }
    }
}
