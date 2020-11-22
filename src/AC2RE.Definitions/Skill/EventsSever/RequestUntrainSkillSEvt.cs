namespace AC2RE.Definitions {

    public class RequestUntrainSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestUntrainSkill;

        // WM_Skill::SendSEvt_RequestUntrainSkill
        public SkillId skillType; // _skillType

        public RequestUntrainSkillSEvt(AC2Reader data) {
            skillType = (SkillId)data.UnpackUInt32();
        }
    }
}
