﻿namespace AC2E.Def {

    public class RequestRaiseSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestRaiseSkill;

        // WM_Skill::SendSEvt_RequestRaiseSkill
        public uint skillType; // _skillType

        public RequestRaiseSkillSEvt(AC2Reader data) {
            skillType = data.UnpackUInt32();
        }
    }
}
