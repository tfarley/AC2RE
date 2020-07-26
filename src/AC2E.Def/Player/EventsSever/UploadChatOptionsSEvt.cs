namespace AC2E.Def {

    public class UploadChatOptionsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__UploadChatOptions;

        // WM_Player::SendSEvt_UploadChatOptions
        public AAHash chatPopupFlags; // _chatPopupFlags
        public AAHash devotedChatWindows; // _devotedChatWindows
        public AAHash chatFontColors; // _chatFontColors
        public AAHash chatFontSizes; // _chatFontSizes
        public AAHash chatFilter; // _chatFilter

        public UploadChatOptionsSEvt(AC2Reader data) {
            chatPopupFlags = data.UnpackPackage<AAHash>();
            devotedChatWindows = data.UnpackPackage<AAHash>();
            chatFontColors = data.UnpackPackage<AAHash>();
            chatFontSizes = data.UnpackPackage<AAHash>();
            chatFilter = data.UnpackPackage<AAHash>();
        }
    }
}
