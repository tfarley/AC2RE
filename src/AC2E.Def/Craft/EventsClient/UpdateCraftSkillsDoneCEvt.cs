namespace AC2E.Def {

    public class UpdateCraftSkillsDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateCraftSkills_Done;

        // WM_Craft::PostCEvt_UpdateCraftSkills_Done
        public RList<CraftSkillRecord> _listCraftSkillRecs;

        public UpdateCraftSkillsDoneCEvt() {

        }

        public UpdateCraftSkillsDoneCEvt(AC2Reader data) {
            _listCraftSkillRecs = data.UnpackPackage<RList<IPackage>>().to<CraftSkillRecord>();
        }

        public void write(AC2Writer data) {
            data.Pack(_listCraftSkillRecs);
        }
    }
}
