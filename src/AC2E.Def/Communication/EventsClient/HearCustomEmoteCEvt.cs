namespace AC2E.Def {

    public class HearCustomEmoteCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__HearCustomEmote;

        // WM_Communication::PostCEvt_CHearTell
        public WPString _text;
        public WPString _senderName;
        public InstanceId _senderID;

        public HearCustomEmoteCEvt() {

        }

        public HearCustomEmoteCEvt(AC2Reader data) {
            _text = data.UnpackPackage<WPString>();
            _senderName = data.UnpackPackage<WPString>();
            _senderID = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_text);
            data.Pack(_senderName);
            data.Pack(_senderID);
        }
    }
}
