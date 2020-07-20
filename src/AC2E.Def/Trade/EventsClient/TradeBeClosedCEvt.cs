namespace AC2E.Def {

    public class TradeBeClosedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeClosed;

        // WM_Trade::PostCEvt_Client_Trade_BeClosed
        public uint _etype;
        public InstanceId _src;

        public TradeBeClosedCEvt() {

        }

        public TradeBeClosedCEvt(AC2Reader data) {
            _etype = data.UnpackUInt32();
            _src = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_etype);
            data.Pack(_src);
        }
    }
}
