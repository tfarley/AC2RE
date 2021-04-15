namespace AC2RE.Definitions {

    public class RequestRaiseSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestRaiseSkill;

        // WM_Skill::SendSEvt_RequestRaiseSkill
        public SkillId skillType; // _skillType

        public RequestRaiseSkillSEvt(AC2Reader data) {
            skillType = (SkillId)data.UnpackUInt32();
        }
    }
}
