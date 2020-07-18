using System.IO;

namespace AC2E.WLib {

    public class UpdateRecipeDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateRecipe_Done;

        // WM_Craft::PostCEvt_UpdateRecipe_Done
        public RecipeRecordPkg _recipeRec;

        public UpdateRecipeDoneCEvt() {

        }

        public UpdateRecipeDoneCEvt(BinaryReader data) {
            _recipeRec = data.UnpackPackage<RecipeRecordPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_recipeRec);
        }
    }
}
