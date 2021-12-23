namespace AC2RE.Definitions;

public class RequestRaiseCraftSkillSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Craft__RequestRaiseCraftSkill;

    // WM_Craft::SendSEvt_RequestRaiseCraftSkill
    public DataId craftSkillDid; // _didCraftSkill

    public RequestRaiseCraftSkillSEvt(AC2Reader data) {
        craftSkillDid = data.UnpackDataId();
    }
}
