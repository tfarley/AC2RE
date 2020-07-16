﻿using System.IO;

namespace AC2E.WLib {

    public class UploadGameOptionsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetShortcut;

        // WM_Player::SendSEvt_UploadGameOptions
        public uint _optionsBitfield;
        public uint _radarMask;
        public int _shortcutHeight;
        public float _damagTextRange;

        public UploadGameOptionsSEvt(BinaryReader data) {
            _optionsBitfield = data.UnpackUInt32();
            _radarMask = data.UnpackUInt32();
            _shortcutHeight = data.UnpackInt32();
            _damagTextRange = data.UnpackSingle();
        }
    }
}
