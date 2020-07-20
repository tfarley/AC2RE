namespace AC2E.Def {

    public class UpdateSkillInfoCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Skill__UpdateInfo;

        // WM_Skill::PostCEvt_Skill_UpdateInfo
        public SkillInfo _info;

        public UpdateSkillInfoCEvt() {

        }

        public UpdateSkillInfoCEvt(AC2Reader data) {
            _info = data.UnpackPackage<SkillInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(_info);
        }
    }
}
