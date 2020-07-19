using System.IO;

namespace AC2E.WLib {

    public class SkillUpdateEverythingCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateEverything;

        // WM_Skill::PostCEvt_Skill_UpdateEverything
        public SkillRepositoryPkg _rep;

        public SkillUpdateEverythingCEvt() {

        }

        public SkillUpdateEverythingCEvt(BinaryReader data) {
            _rep = data.UnpackPackage<SkillRepositoryPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_rep);
        }
    }
}
