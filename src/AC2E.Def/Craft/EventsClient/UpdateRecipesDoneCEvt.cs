namespace AC2E.Def {

    public class UpdateRecipesDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateRecipes_Done;

        // WM_Craft::PostCEvt_UpdateRecipes_Done
        public RList<RecipeRecord> recipeRecords; // _listRecipeRecs

        public UpdateRecipesDoneCEvt() {

        }

        public UpdateRecipesDoneCEvt(AC2Reader data) {
            recipeRecords = data.UnpackPackage<RList<IPackage>>().to<RecipeRecord>();
        }

        public void write(AC2Writer data) {
            data.Pack(recipeRecords);
        }
    }
}
