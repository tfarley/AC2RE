namespace AC2E.Def {

    public class UpdateSkillRepositoryCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateRepository;

        // WM_Skill::PostCEvt_Skill_UpdateRepository
        public SkillRepository _rep;

        public UpdateSkillRepositoryCEvt() {

        }

        public UpdateSkillRepositoryCEvt(AC2Reader data) {
            _rep = data.UnpackPackage<SkillRepository>();
        }

        public void write(AC2Writer data) {
            data.Pack(_rep);
        }
    }
}
