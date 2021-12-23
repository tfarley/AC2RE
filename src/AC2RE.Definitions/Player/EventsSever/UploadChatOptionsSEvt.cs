using System.Collections.Generic;

namespace AC2RE.Definitions;

public class UploadChatOptionsSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Player__UploadChatOptions;

    // WM_Player::SendSEvt_UploadChatOptions
    public Dictionary<uint, uint> chatPopupFlags; // _chatPopupFlags
    public Dictionary<uint, uint> devotedChatWindows; // _devotedChatWindows
    public Dictionary<uint, uint> chatFontColors; // _chatFontColors
    public Dictionary<uint, uint> chatFontSizes; // _chatFontSizes
    public Dictionary<uint, uint> chatFilter; // _chatFilter

    public UploadChatOptionsSEvt(AC2Reader data) {
        chatPopupFlags = data.UnpackPackage<AAHash>();
        devotedChatWindows = data.UnpackPackage<AAHash>();
        chatFontColors = data.UnpackPackage<AAHash>();
        chatFontSizes = data.UnpackPackage<AAHash>();
        chatFilter = data.UnpackPackage<AAHash>();
    }
}
