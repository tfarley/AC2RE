﻿namespace AC2E.Def {

    public class TradeBeRefreshedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeRefreshed;

        // WM_Trade::PostCEvt_Client_Trade_BeRefreshed
        public InstanceId _src;

        public TradeBeRefreshedCEvt() {

        }

        public TradeBeRefreshedCEvt(AC2Reader data) {
            _src = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_src);
        }
    }
}