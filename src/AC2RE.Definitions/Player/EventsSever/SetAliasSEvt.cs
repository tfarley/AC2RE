namespace AC2RE.Definitions;

public class SetAliasSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Player__SetAlias;

    // WM_Player::SendSEvt_SetAlias
    public bool add; // _add
    public WPString text; // _text
    public WPString alias; // _alias

    public SetAliasSEvt(AC2Reader data) {
        add = data.UnpackBoolean();
        text = data.UnpackPackage<WPString>();
        alias = data.UnpackPackage<WPString>();
    }
}
