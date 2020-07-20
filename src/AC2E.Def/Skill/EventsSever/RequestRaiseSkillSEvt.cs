namespace AC2E.Def {

    public class RequestRaiseSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestRaiseSkill;

        // WM_Skill::SendSEvt_RequestRaiseSkill
        public uint _skillType;

        public RequestRaiseSkillSEvt(AC2Reader data) {
            _skillType = data.UnpackUInt32();
        }
    }
}
