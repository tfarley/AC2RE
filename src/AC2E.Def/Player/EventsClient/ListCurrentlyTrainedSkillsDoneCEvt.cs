namespace AC2E.Def {

    public class ListCurrentlyTrainedSkillsDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ListCurrentlyTrainedSkills_Done;

        // WM_Player::PostCEvt_ListCurrentlyTrainedSkills_Done
        public RList<StringInfo> skillNames; // _skills

        public ListCurrentlyTrainedSkillsDoneCEvt() {

        }

        public ListCurrentlyTrainedSkillsDoneCEvt(AC2Reader data) {
            skillNames = data.UnpackPackage<RList<IPackage>>().to<StringInfo>();
        }

        public void write(AC2Writer data) {
            data.Pack(skillNames);
        }
    }
}
