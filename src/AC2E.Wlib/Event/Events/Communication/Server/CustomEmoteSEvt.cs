﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class CustomEmoteSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__CustomEmote;

        // WM_Communication::SendSEvt_CustomEmote
        public WPString _text;

        public CustomEmoteSEvt(BinaryReader data) {
            _text = data.UnpackPackage<WPString>();
        }
    }
}
