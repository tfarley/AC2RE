namespace AC2E.Def {

    public class DoSayCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__CDoSay;

        // WM_Communication::PostCEvt_CDoSay
        public uint _weenieChatFlags;
        public StringInfo _msg;

        public DoSayCEvt() {

        }

        public DoSayCEvt(AC2Reader data) {
            _weenieChatFlags = data.UnpackUInt32();
            _msg = data.UnpackPackage<StringInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(_weenieChatFlags);
            data.Pack(_msg);
        }
    }
}
