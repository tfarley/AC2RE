namespace AC2E.Def {

    public class TradeBeOfferedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeOffered;

        // WM_Trade::PostCEvt_Client_Trade_BeOffered
        public uint amount; // _amt
        public InstanceId itemId; // _item
        public InstanceId sourceId; // _src

        public TradeBeOfferedCEvt() {

        }

        public TradeBeOfferedCEvt(AC2Reader data) {
            amount = data.UnpackUInt32();
            itemId = data.UnpackInstanceId();
            sourceId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(amount);
            data.Pack(itemId);
            data.Pack(sourceId);
        }
    }
}
