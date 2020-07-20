namespace AC2E.Def {

    public class CharacterRenameSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__CharacterRename;

        // WM_Player::SendSEvt_CharacterRename
        public WPString _name;

        public CharacterRenameSEvt(AC2Reader data) {
            _name = data.UnpackPackage<WPString>();
        }
    }
}
