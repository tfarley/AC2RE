namespace AC2E.Def {

    public class DisplayStringInfoCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__DisplayStringInfo;

        // WM_Communication::PostCEvt_DisplayStringInfo
        public StringInfo _msg;
        public TextType _type;

        public DisplayStringInfoCEvt() {

        }

        public DisplayStringInfoCEvt(AC2Reader data) {
            _msg = data.UnpackPackage<StringInfo>();
            _type = (TextType)data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_msg);
            data.Pack((uint)_type);
        }
    }
}
