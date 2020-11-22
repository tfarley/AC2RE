namespace AC2RE.Definitions {

    public class UpdateCraftSkillDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateCraftSkill_Done;

        // WM_Craft::PostCEvt_UpdateCraftSkill_Done
        public CraftSkillRecord craftSkillRecord; // _craftSkillRec

        public UpdateCraftSkillDoneCEvt() {

        }

        public UpdateCraftSkillDoneCEvt(AC2Reader data) {
            craftSkillRecord = data.UnpackPackage<CraftSkillRecord>();
        }

        public void write(AC2Writer data) {
            data.Pack(craftSkillRecord);
        }
    }
}
