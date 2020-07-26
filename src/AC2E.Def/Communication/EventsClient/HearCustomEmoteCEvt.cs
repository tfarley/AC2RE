namespace AC2E.Def {

    public class HearCustomEmoteCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__HearCustomEmote;

        // WM_Communication::PostCEvt_CHearTell
        public WPString text; // _text
        public WPString senderName; // _senderName
        public InstanceId senderId; // _senderID

        public HearCustomEmoteCEvt() {

        }

        public HearCustomEmoteCEvt(AC2Reader data) {
            text = data.UnpackPackage<WPString>();
            senderName = data.UnpackPackage<WPString>();
            senderId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(text);
            data.Pack(senderName);
            data.Pack(senderId);
        }
    }
}
