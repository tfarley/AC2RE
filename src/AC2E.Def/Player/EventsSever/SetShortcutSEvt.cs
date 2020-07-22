﻿namespace AC2E.Def {

    public class SetShortcutSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetShortcut;

        // WM_Player::SendSEvt_SetShortcut
        public ShortcutInfo _shortcut;
        public uint _index;

        public SetShortcutSEvt(AC2Reader data) {
            _shortcut = data.UnpackPackage<ShortcutInfo>();
            _index = data.UnpackUInt32();
        }
    }
}