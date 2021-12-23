namespace AC2RE.Definitions;

public class UpdateRecipeDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateRecipe_Done;

    // WM_Craft::PostCEvt_UpdateRecipe_Done
    public RecipeRecord recipeRecord; // _recipeRec

    public UpdateRecipeDoneCEvt() {

    }

    public UpdateRecipeDoneCEvt(AC2Reader data) {
        recipeRecord = data.UnpackPackage<RecipeRecord>();
    }

    public void write(AC2Writer data) {
        data.Pack(recipeRecord);
    }
}
