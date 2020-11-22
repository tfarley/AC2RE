namespace AC2RE.Definitions {

    public class CustomEmoteSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Communication__CustomEmote;

        // WM_Communication::SendSEvt_CustomEmote
        public WPString text; // _text

        public CustomEmoteSEvt(AC2Reader data) {
            text = data.UnpackPackage<WPString>();
        }
    }
}
