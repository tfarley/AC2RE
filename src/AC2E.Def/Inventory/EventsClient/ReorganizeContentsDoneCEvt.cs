using System.IO;

namespace AC2E.Def {

    public class ReorganizeContentsDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__ReorganizeContents_Done;

        // WM_Inventory::PostCEvt_ReorganizeContents_Done
        public InvMoveDesc _iDesc;

        public ReorganizeContentsDoneCEvt() {

        }

        public ReorganizeContentsDoneCEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvMoveDesc>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iDesc);
        }
    }
}
