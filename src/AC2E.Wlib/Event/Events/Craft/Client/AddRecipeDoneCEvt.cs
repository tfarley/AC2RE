using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class AddRecipeDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__AddRecipe_Done;

        // CPlayer::RecvCEvt_AddRecipe_Done
        public DataId didRecipe;

        public AddRecipeDoneCEvt() {

        }

        public AddRecipeDoneCEvt(BinaryReader data) {
            didRecipe = data.UnpackDataId();
        }

        public void write(BinaryWriter data) {
            data.Pack(didRecipe);
        }
    }
}
