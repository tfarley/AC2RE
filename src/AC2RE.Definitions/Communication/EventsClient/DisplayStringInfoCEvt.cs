namespace AC2RE.Definitions {

    public class DisplayStringInfoCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Communication__DisplayStringInfo;

        // WM_Communication::PostCEvt_DisplayStringInfo
        public StringInfo text; // _msg
        public TextType type; // _type

        public DisplayStringInfoCEvt() {

        }

        public DisplayStringInfoCEvt(AC2Reader data) {
            text = data.UnpackPackage<StringInfo>();
            type = (TextType)data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(text);
            data.Pack((uint)type);
        }
    }
}
