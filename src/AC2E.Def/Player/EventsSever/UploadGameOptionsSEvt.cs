namespace AC2E.Def {

    public class UploadGameOptionsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetShortcut;

        // WM_Player::SendSEvt_UploadGameOptions
        public uint optionsBitfield; // _optionsBitfield
        public uint radarMask; // _radarMask
        public int shortcutHeight; // _shortcutHeight
        public float damageTextRange; // _damagTextRange

        public UploadGameOptionsSEvt(AC2Reader data) {
            optionsBitfield = data.UnpackUInt32();
            radarMask = data.UnpackUInt32();
            shortcutHeight = data.UnpackInt32();
            damageTextRange = data.UnpackSingle();
        }
    }
}
