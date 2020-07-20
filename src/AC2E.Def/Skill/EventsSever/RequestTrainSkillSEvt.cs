namespace AC2E.Def {

    public class RequestTrainSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestTrainSkill;

        // WM_Skill::SendSEvt_RequestTrainSkill
        public uint _skillType;

        public RequestTrainSkillSEvt(AC2Reader data) {
            _skillType = data.UnpackUInt32();
        }
    }
}
