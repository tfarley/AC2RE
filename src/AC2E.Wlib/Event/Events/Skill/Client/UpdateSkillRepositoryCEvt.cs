using System.IO;

namespace AC2E.WLib {

    public class UpdateSkillRepositoryCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateRepository;

        // WM_Skill::PostCEvt_Skill_UpdateRepository
        public SkillRepositoryPkg _rep;

        public UpdateSkillRepositoryCEvt() {

        }

        public UpdateSkillRepositoryCEvt(BinaryReader data) {
            _rep = data.UnpackPackage<SkillRepositoryPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_rep);
        }
    }
}
