﻿namespace AC2E.Def {

    public class UpdateCraftSkillDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateCraftSkill_Done;

        // WM_Craft::PostCEvt_UpdateCraftSkill_Done
        public CraftSkillRecord _craftSkillRec;

        public UpdateCraftSkillDoneCEvt() {

        }

        public UpdateCraftSkillDoneCEvt(AC2Reader data) {
            _craftSkillRec = data.UnpackPackage<CraftSkillRecord>();
        }

        public void write(AC2Writer data) {
            data.Pack(_craftSkillRec);
        }
    }
}