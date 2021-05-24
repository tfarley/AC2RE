namespace AC2RE.Definitions {

    public class RequestUntrainSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestUntrainSkill;

        // WM_Skill::SendSEvt_RequestUntrainSkill
        public SkillId skillId; // _skillType

        public RequestUntrainSkillSEvt(AC2Reader data) {
            skillId = (SkillId)data.UnpackUInt32();
        }
    }
}
