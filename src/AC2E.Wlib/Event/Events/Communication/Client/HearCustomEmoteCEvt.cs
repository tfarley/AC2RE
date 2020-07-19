﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class HearCustomEmoteCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__HearCustomEmote;

        // WM_Communication::PostCEvt_CHearTell
        public WPString _text;
        public WPString _senderName;
        public InstanceId _senderID;

        public HearCustomEmoteCEvt() {

        }

        public HearCustomEmoteCEvt(BinaryReader data) {
            _text = data.UnpackPackage<WPString>();
            _senderName = data.UnpackPackage<WPString>();
            _senderID = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_text);
            data.Pack(_senderName);
            data.Pack(_senderID);
        }
    }
}
