namespace AC2E.Def {

    public class TradeBeRevokedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeRevoked;

        // WM_Trade::PostCEvt_Client_Trade_BeRevoked
        public InstanceId _item;
        public InstanceId _src;

        public TradeBeRevokedCEvt() {

        }

        public TradeBeRevokedCEvt(AC2Reader data) {
            _item = data.UnpackInstanceId();
            _src = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_item);
            data.Pack(_src);
        }
    }
}
