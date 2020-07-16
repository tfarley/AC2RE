using System.IO;

namespace AC2E.WLib {

    public class ReorganizeContentsDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__ReorganizeContents_Done;

        // WM_Inventory::PostCEvt_ReorganizeContents_Done
        public InvMoveDescPkg _iDesc;

        public ReorganizeContentsDoneCEvt() {

        }

        public ReorganizeContentsDoneCEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvMoveDescPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iDesc);
        }
    }
}
