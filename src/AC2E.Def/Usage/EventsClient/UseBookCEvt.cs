namespace AC2E.Def {

    public class UseBookCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Usage__UseBook;

        // WM_Usage::PostCEvt_Usage_UseBook
        public DataId imageDid; // _didImage
        public bool showControls; // _bShowControls
        public StringInfo bookText; // _siBookSource

        public UseBookCEvt() {

        }

        public UseBookCEvt(AC2Reader data) {
            imageDid = data.UnpackDataId();
            showControls = data.UnpackBoolean();
            bookText = data.UnpackPackage<StringInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(imageDid);
            data.Pack(showControls);
            data.Pack(bookText);
        }
    }
}
