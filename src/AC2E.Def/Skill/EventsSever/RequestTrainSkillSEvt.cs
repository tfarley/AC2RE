namespace AC2E.Def {

    public class RequestTrainSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestTrainSkill;

        // WM_Skill::SendSEvt_RequestTrainSkill
        public uint skillType; // _skillType

        public RequestTrainSkillSEvt(AC2Reader data) {
            skillType = data.UnpackUInt32();
        }
    }
}
