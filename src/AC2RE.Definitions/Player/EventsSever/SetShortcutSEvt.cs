namespace AC2RE.Definitions {

    public class SetShortcutSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetShortcut;

        // WM_Player::SendSEvt_SetShortcut
        public ShortcutInfo shortcut; // _shortcut
        public uint index; // _index

        public SetShortcutSEvt(AC2Reader data) {
            shortcut = data.UnpackPackage<ShortcutInfo>();
            index = data.UnpackUInt32();
        }
    }
}
