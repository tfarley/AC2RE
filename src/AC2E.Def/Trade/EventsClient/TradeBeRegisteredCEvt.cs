﻿namespace AC2E.Def {

    public class TradeBeRegisteredCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Trade__BeRegistered;

        // WM_Trade::PostCEvt_Client_Trade_BeRegistered
        public StringInfo partnerName; // _partner_name
        public InstanceId slaveId; // _slave
        public InstanceId masterId; // _master

        public TradeBeRegisteredCEvt() {

        }

        public TradeBeRegisteredCEvt(AC2Reader data) {
            partnerName = data.UnpackPackage<StringInfo>();
            slaveId = data.UnpackInstanceId();
            masterId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(partnerName);
            data.Pack(slaveId);
            data.Pack(masterId);
        }
    }
}
