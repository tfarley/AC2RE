using System.IO;

namespace AC2E.Def {

    public class AddIngredientDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__AddIngredient_Done;

        // WM_Craft::PostCEvt_AddIngredient_Done
        public uint _err;
        public uint _uiSpinnerVal;
        public InstanceId _iidIngredient;
        public uint _uiOrdinal;
        public DataId _didRecipe;

        public AddIngredientDoneCEvt() {

        }

        public AddIngredientDoneCEvt(BinaryReader data) {
            _err = data.UnpackUInt32();
            _uiSpinnerVal = data.UnpackUInt32();
            _iidIngredient = data.UnpackInstanceId();
            _uiOrdinal = data.UnpackUInt32();
            _didRecipe = data.UnpackDataId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_err);
            data.Pack(_uiSpinnerVal);
            data.Pack(_iidIngredient);
            data.Pack(_uiOrdinal);
            data.Pack(_didRecipe);
        }
    }
}
