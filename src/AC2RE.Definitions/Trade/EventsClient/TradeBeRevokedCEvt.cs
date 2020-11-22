namespace AC2RE.Definitions {

    public class TradeBeRevokedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeRevoked;

        // WM_Trade::PostCEvt_Client_Trade_BeRevoked
        public InstanceId itemId; // _item
        public InstanceId sourceId; // _src

        public TradeBeRevokedCEvt() {

        }

        public TradeBeRevokedCEvt(AC2Reader data) {
            itemId = data.UnpackInstanceId();
            sourceId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(itemId);
            data.Pack(sourceId);
        }
    }
}
