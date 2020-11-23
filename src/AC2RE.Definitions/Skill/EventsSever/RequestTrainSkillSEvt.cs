﻿namespace AC2RE.Definitions {

    public class RequestTrainSkillSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Skill__RequestTrainSkill;

        // WM_Skill::SendSEvt_RequestTrainSkill
        public SkillId skillType; // _skillType

        public RequestTrainSkillSEvt(AC2Reader data) {
            skillType = (SkillId)data.UnpackUInt32();
        }
    }
}