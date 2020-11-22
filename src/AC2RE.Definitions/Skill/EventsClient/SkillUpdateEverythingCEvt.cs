namespace AC2RE.Definitions {

    public class SkillUpdateEverythingCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateEverything;

        // WM_Skill::PostCEvt_Skill_UpdateEverything
        public SkillRepository skillRepository; // _rep

        public SkillUpdateEverythingCEvt() {

        }

        public SkillUpdateEverythingCEvt(AC2Reader data) {
            skillRepository = data.UnpackPackage<SkillRepository>();
        }

        public void write(AC2Writer data) {
            data.Pack(skillRepository);
        }
    }
}
