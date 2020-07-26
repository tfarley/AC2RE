﻿namespace AC2E.Def {

    public class DisplayKillingMessageCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__DisplayKillingMsg;

        // WM_Death::PostCEvt_DeathSystem_DisplayKillingMsg
        public StringInfo defenderName; // _siDefender

        public DisplayKillingMessageCEvt() {

        }

        public DisplayKillingMessageCEvt(AC2Reader data) {
            defenderName = data.UnpackPackage<StringInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(defenderName);
        }
    }
}
