namespace AC2E.Def {

    public class RequestAddIngredientSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Craft__RequestAddIngredient;

        // WM_Craft::SendSEvt_RequestAddIngredient
        public uint spinnerVal; // _uiSpinnerVal
        public InstanceId ingredientId; // _iidIngredient
        public uint ordinal; // _uiOrdinal
        public DataId recipeDid; // _didRecipe

        public RequestAddIngredientSEvt(AC2Reader data) {
            spinnerVal = data.UnpackUInt32();
            ingredientId = data.UnpackInstanceId();
            ordinal = data.UnpackUInt32();
            recipeDid = data.UnpackDataId();
        }
    }
}
