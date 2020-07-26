﻿namespace AC2E.Def {

    public class RequestVassalCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__RequestVassal;

        // WM_Allegiance::PostCEvt_RequestVassal
        public StringInfo vassalName; // _vassal_name
        public InstanceId vassalId; // _vassal

        public RequestVassalCEvt() {

        }

        public RequestVassalCEvt(AC2Reader data) {
            vassalName = data.UnpackPackage<StringInfo>();
            vassalId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(vassalName);
            data.Pack(vassalId);
        }
    }
}
