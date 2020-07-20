namespace AC2E.Def {

    public class RequestRaiseCraftSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Craft__RequestRaiseCraftSkill;

        // WM_Craft::SendSEvt_RequestRaiseCraftSkill
        public DataId _didCraftSkill;

        public RequestRaiseCraftSkillSEvt(AC2Reader data) {
            _didCraftSkill = data.UnpackDataId();
        }
    }
}
