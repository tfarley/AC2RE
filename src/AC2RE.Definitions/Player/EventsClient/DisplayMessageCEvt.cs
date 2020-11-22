namespace AC2RE.Definitions {

    public class DisplayMessageCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__DisplayMessage;

        // WM_Player::PostCEvt_DisplayMessage
        public bool topmost; // _topmost
        public StringInfo text; // _msg

        public DisplayMessageCEvt() {

        }

        public DisplayMessageCEvt(AC2Reader data) {
            topmost = data.UnpackBoolean();
            text = data.UnpackPackage<StringInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(topmost);
            data.Pack(text);
        }
    }
}
