using System.IO;

namespace AC2E.Def {

    public class UpdateRecipeDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateRecipe_Done;

        // WM_Craft::PostCEvt_UpdateRecipe_Done
        public RecipeRecord _recipeRec;

        public UpdateRecipeDoneCEvt() {

        }

        public UpdateRecipeDoneCEvt(BinaryReader data) {
            _recipeRec = data.UnpackPackage<RecipeRecord>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_recipeRec);
        }
    }
}
