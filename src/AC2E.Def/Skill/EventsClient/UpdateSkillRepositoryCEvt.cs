using System.IO;

namespace AC2E.Def {

    public class UpdateSkillRepositoryCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateRepository;

        // WM_Skill::PostCEvt_Skill_UpdateRepository
        public SkillRepository _rep;

        public UpdateSkillRepositoryCEvt() {

        }

        public UpdateSkillRepositoryCEvt(BinaryReader data) {
            _rep = data.UnpackPackage<SkillRepository>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_rep);
        }
    }
}
