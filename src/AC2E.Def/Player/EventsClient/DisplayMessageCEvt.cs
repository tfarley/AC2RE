namespace AC2E.Def {

    public class DisplayMessageCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__DisplayMessage;

        // WM_Player::PostCEvt_DisplayMessage
        public bool _topmost;
        public StringInfo _msg;

        public DisplayMessageCEvt() {

        }

        public DisplayMessageCEvt(AC2Reader data) {
            _topmost = data.UnpackBoolean();
            _msg = data.UnpackPackage<StringInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(_topmost);
            data.Pack(_msg);
        }
    }
}
