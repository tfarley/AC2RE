﻿namespace AC2E.Def {

    public class RemoveSkillInfoCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__RemoveInfo;

        // WM_Skill::PostCEvt_Skill_RemoveInfo
        public uint _skillType;

        public RemoveSkillInfoCEvt() {

        }

        public RemoveSkillInfoCEvt(AC2Reader data) {
            _skillType = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_skillType);
        }
    }
}