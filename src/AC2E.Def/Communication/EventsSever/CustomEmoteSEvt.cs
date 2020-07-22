﻿namespace AC2E.Def {

    public class CustomEmoteSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__CustomEmote;

        // WM_Communication::SendSEvt_CustomEmote
        public WPString _text;

        public CustomEmoteSEvt(AC2Reader data) {
            _text = data.UnpackPackage<WPString>();
        }
    }
}