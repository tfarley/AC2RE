namespace AC2E.Def {

    public class TradeBeAcceptedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeAccepted;

        // WM_Trade::PostCEvt_Client_Trade_BeAccepted
        public InstanceId sourceId; // _src

        public TradeBeAcceptedCEvt() {

        }

        public TradeBeAcceptedCEvt(AC2Reader data) {
            sourceId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(sourceId);
        }
    }
}
