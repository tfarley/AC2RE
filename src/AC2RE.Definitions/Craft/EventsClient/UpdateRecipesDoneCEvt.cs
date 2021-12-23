using System.Collections.Generic;

namespace AC2RE.Definitions;

public class UpdateRecipesDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateRecipes_Done;

    // WM_Craft::PostCEvt_UpdateRecipes_Done
    public List<RecipeRecord> recipeRecords; // _listRecipeRecs

    public UpdateRecipesDoneCEvt() {

    }

    public UpdateRecipesDoneCEvt(AC2Reader data) {
        recipeRecords = data.UnpackPackage<RList>().to<RecipeRecord>();
    }

    public void write(AC2Writer data) {
        data.Pack(RList.from(recipeRecords));
    }
}
