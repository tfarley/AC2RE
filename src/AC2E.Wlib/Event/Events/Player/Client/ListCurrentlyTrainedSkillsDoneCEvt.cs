using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class ListCurrentlyTrainedSkillsDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ListCurrentlyTrainedSkills_Done;

        // WM_Player::PostCEvt_ListCurrentlyTrainedSkills_Done
        public RList<StringInfo> _skills;

        public ListCurrentlyTrainedSkillsDoneCEvt() {

        }

        public ListCurrentlyTrainedSkillsDoneCEvt(BinaryReader data) {
            _skills = data.UnpackPackage<RList<IPackage>>().to<StringInfo>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_skills);
        }
    }
}
