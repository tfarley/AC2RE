namespace AC2RE.Definitions;

public class AddIngredientDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__AddIngredient_Done;

    // WM_Craft::PostCEvt_AddIngredient_Done
    public ErrorType error; // _err
    public uint spinnerVal; // _uiSpinnerVal
    public InstanceId ingredientId; // _iidIngredient
    public uint ordinal; // _uiOrdinal
    public DataId recipeDid; // _didRecipe

    public AddIngredientDoneCEvt() {

    }

    public AddIngredientDoneCEvt(AC2Reader data) {
        error = data.UnpackEnum<ErrorType>();
        spinnerVal = data.UnpackUInt32();
        ingredientId = data.UnpackInstanceId();
        ordinal = data.UnpackUInt32();
        recipeDid = data.UnpackDataId();
    }

    public void write(AC2Writer data) {
        data.PackEnum(error);
        data.Pack(spinnerVal);
        data.Pack(ingredientId);
        data.Pack(ordinal);
        data.Pack(recipeDid);
    }
}
