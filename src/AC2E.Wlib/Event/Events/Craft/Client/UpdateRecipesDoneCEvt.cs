using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UpdateRecipesDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateRecipes_Done;

        // WM_Craft::PostCEvt_UpdateRecipes_Done
        public RList<RecipeRecordPkg> _listRecipeRecs;

        public UpdateRecipesDoneCEvt() {

        }

        public UpdateRecipesDoneCEvt(BinaryReader data) {
            _listRecipeRecs = data.UnpackPackage<RList<IPackage>>().to<RecipeRecordPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_listRecipeRecs);
        }
    }
}
