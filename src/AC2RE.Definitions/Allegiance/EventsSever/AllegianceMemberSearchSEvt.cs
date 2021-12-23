namespace AC2RE.Definitions;

public class AllegianceMemberSearchSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceMemberSearch;

    // WM_Allegiance::SendSEvt_AllegianceMemberSearch
    public WPString memberName; // _member

    public AllegianceMemberSearchSEvt(AC2Reader data) {
        memberName = data.UnpackPackage<WPString>();
    }
}
