namespace AC2E.Def {

    public class UploadChatOptionsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__UploadChatOptions;

        // WM_Player::SendSEvt_UploadChatOptions
        public AAHash _chatPopupFlags;
        public AAHash _devotedChatWindows;
        public AAHash _chatFontColors;
        public AAHash _chatFontSizes;
        public AAHash _chatFilter;

        public UploadChatOptionsSEvt(AC2Reader data) {
            _chatPopupFlags = data.UnpackPackage<AAHash>();
            _devotedChatWindows = data.UnpackPackage<AAHash>();
            _chatFontColors = data.UnpackPackage<AAHash>();
            _chatFontSizes = data.UnpackPackage<AAHash>();
            _chatFilter = data.UnpackPackage<AAHash>();
        }
    }
}
