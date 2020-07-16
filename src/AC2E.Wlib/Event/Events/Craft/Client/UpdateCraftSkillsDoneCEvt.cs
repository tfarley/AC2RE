using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdateCraftSkillsDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateCraftSkills_Done;

        // WM_Craft::PostCEvt_UpdateCraftSkills_Done
        public RList<CraftSkillRecordPkg> _listCraftSkillRecs;

        public UpdateCraftSkillsDoneCEvt() {

        }

        public UpdateCraftSkillsDoneCEvt(BinaryReader data) {
            _listCraftSkillRecs = data.UnpackPackage<RList<IPackage>>().to<CraftSkillRecordPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_listCraftSkillRecs);
        }
    }
}
