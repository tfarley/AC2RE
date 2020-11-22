namespace AC2RE.Definitions {

    public class UpdateSkillRepositoryCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateRepository;

        // WM_Skill::PostCEvt_Skill_UpdateRepository
        public SkillRepository skillRepository; // _rep

        public UpdateSkillRepositoryCEvt() {

        }

        public UpdateSkillRepositoryCEvt(AC2Reader data) {
            skillRepository = data.UnpackPackage<SkillRepository>();
        }

        public void write(AC2Writer data) {
            data.Pack(skillRepository);
        }
    }
}
