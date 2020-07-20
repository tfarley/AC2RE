namespace AC2E.Def {

    public class TradeBeRegisteredCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeRegistered;

        // WM_Trade::PostCEvt_Client_Trade_BeRegistered
        public StringInfo _partner_name;
        public InstanceId _slave;
        public InstanceId _master;

        public TradeBeRegisteredCEvt() {

        }

        public TradeBeRegisteredCEvt(AC2Reader data) {
            _partner_name = data.UnpackPackage<StringInfo>();
            _slave = data.UnpackInstanceId();
            _master = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_partner_name);
            data.Pack(_slave);
            data.Pack(_master);
        }
    }
}
