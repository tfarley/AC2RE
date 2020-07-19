using System.IO;

namespace AC2E.Def {

    public class UpdateCraftSkillsDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateCraftSkills_Done;

        // WM_Craft::PostCEvt_UpdateCraftSkills_Done
        public RList<CraftSkillRecord> _listCraftSkillRecs;

        public UpdateCraftSkillsDoneCEvt() {

        }

        public UpdateCraftSkillsDoneCEvt(BinaryReader data) {
            _listCraftSkillRecs = data.UnpackPackage<RList<IPackage>>().to<CraftSkillRecord>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_listCraftSkillRecs);
        }
    }
}
