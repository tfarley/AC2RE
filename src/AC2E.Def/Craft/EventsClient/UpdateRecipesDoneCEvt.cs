namespace AC2E.Def {

    public class UpdateRecipesDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateRecipes_Done;

        // WM_Craft::PostCEvt_UpdateRecipes_Done
        public RList<RecipeRecord> _listRecipeRecs;

        public UpdateRecipesDoneCEvt() {

        }

        public UpdateRecipesDoneCEvt(AC2Reader data) {
            _listRecipeRecs = data.UnpackPackage<RList<IPackage>>().to<RecipeRecord>();
        }

        public void write(AC2Writer data) {
            data.Pack(_listRecipeRecs);
        }
    }
}
