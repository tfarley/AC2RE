namespace AC2E.Def {

    public class RequestUntrainSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestUntrainSkill;

        // WM_Skill::SendSEvt_RequestUntrainSkill
        public uint skillType; // _skillType

        public RequestUntrainSkillSEvt(AC2Reader data) {
            skillType = data.UnpackUInt32();
        }
    }
}
