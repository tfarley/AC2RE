namespace AC2E.Def {

    public class SetAliasSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetAlias;

        // WM_Player::SendSEvt_SetAlias
        public bool _add;
        public WPString _text;
        public WPString _alias;

        public SetAliasSEvt(AC2Reader data) {
            _add = data.UnpackBoolean();
            _text = data.UnpackPackage<WPString>();
            _alias = data.UnpackPackage<WPString>();
        }
    }
}
