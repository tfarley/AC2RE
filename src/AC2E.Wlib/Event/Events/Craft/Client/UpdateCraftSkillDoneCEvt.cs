using System.IO;

namespace AC2E.WLib {

    public class UpdateCraftSkillDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateCraftSkill_Done;

        // WM_Craft::PostCEvt_UpdateCraftSkill_Done
        public CraftSkillRecordPkg _craftSkillRec;

        public UpdateCraftSkillDoneCEvt() {

        }

        public UpdateCraftSkillDoneCEvt(BinaryReader data) {
            _craftSkillRec = data.UnpackPackage<CraftSkillRecordPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_craftSkillRec);
        }
    }
}
