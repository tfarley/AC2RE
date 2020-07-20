namespace AC2E.Def {

    public class SkillUpdateEverythingCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateEverything;

        // WM_Skill::PostCEvt_Skill_UpdateEverything
        public SkillRepository _rep;

        public SkillUpdateEverythingCEvt() {

        }

        public SkillUpdateEverythingCEvt(AC2Reader data) {
            _rep = data.UnpackPackage<SkillRepository>();
        }

        public void write(AC2Writer data) {
            data.Pack(_rep);
        }
    }
}
