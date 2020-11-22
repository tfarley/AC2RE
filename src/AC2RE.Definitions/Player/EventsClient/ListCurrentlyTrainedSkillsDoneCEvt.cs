using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ListCurrentlyTrainedSkillsDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ListCurrentlyTrainedSkills_Done;

        // WM_Player::PostCEvt_ListCurrentlyTrainedSkills_Done
        public List<StringInfo> skillNames; // _skills

        public ListCurrentlyTrainedSkillsDoneCEvt() {

        }

        public ListCurrentlyTrainedSkillsDoneCEvt(AC2Reader data) {
            skillNames = data.UnpackPackage<RList>().to<StringInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(RList.from(skillNames));
        }
    }
}
