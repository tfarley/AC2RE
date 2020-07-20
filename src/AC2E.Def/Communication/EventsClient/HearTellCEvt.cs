﻿namespace AC2E.Def {

    public class HearTellCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__CHearTell;

        // WM_Communication::PostCEvt_CHearTell
        public uint _weenieChatFlags;
        public StringInfo _msg;
        public StringInfo _teller;
        public InstanceId _tellerID;

        public HearTellCEvt() {

        }

        public HearTellCEvt(AC2Reader data) {
            _weenieChatFlags = data.UnpackUInt32();
            _msg = data.UnpackPackage<StringInfo>();
            _teller = data.UnpackPackage<StringInfo>();
            _tellerID = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_weenieChatFlags);
            data.Pack(_msg);
            data.Pack(_teller);
            data.Pack(_tellerID);
        }
    }
}
