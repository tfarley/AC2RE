namespace AC2E.Def {

    public class RemoveRecipeDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__RemoveRecipe_Done;

        // CPlayer::RecvCEvt_RemoveRecipe_Done
        public DataId didRecipe;

        public RemoveRecipeDoneCEvt() {

        }

        public RemoveRecipeDoneCEvt(AC2Reader data) {
            didRecipe = data.UnpackDataId();
        }

        public void write(AC2Writer data) {
            data.Pack(didRecipe);
        }
    }
}
