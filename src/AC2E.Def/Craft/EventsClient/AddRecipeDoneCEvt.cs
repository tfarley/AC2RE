namespace AC2E.Def {

    public class AddRecipeDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__AddRecipe_Done;

        // CPlayer::RecvCEvt_AddRecipe_Done
        public DataId didRecipe;

        public AddRecipeDoneCEvt() {

        }

        public AddRecipeDoneCEvt(AC2Reader data) {
            didRecipe = data.UnpackDataId();
        }

        public void write(AC2Writer data) {
            data.Pack(didRecipe);
        }
    }
}
