using System.IO;

namespace AC2E.Def {

    public class RemoveRecipeDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__RemoveRecipe_Done;

        // CPlayer::RecvCEvt_RemoveRecipe_Done
        public DataId didRecipe;

        public RemoveRecipeDoneCEvt() {

        }

        public RemoveRecipeDoneCEvt(BinaryReader data) {
            didRecipe = data.UnpackDataId();
        }

        public void write(BinaryWriter data) {
            data.Pack(didRecipe);
        }
    }
}
