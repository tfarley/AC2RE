namespace AC2E.Def {

    public class UseBookCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Usage__UseBook;

        // WM_Usage::PostCEvt_Usage_UseBook
        public DataId _didImage;
        public bool _bShowControls;
        public StringInfo _siBookSource;

        public UseBookCEvt() {

        }

        public UseBookCEvt(AC2Reader data) {
            _didImage = data.UnpackDataId();
            _bShowControls = data.UnpackUInt32() != 0;
            _siBookSource = data.UnpackPackage<StringInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(_didImage);
            data.Pack(_bShowControls ? (uint)1 : (uint)0);
            data.Pack(_siBookSource);
        }
    }
}
