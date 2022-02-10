namespace AC2RE.Definitions;

public class AllegianceRenameSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceRename;

    // WM_Allegiance::SendSEvt_AllegianceRename
    public WPString name; // _name

    public AllegianceRenameSEvt(AC2Reader data) {
        name = data.UnpackHeapObject<WPString>();
    }
}
