using System.IO;

namespace AC2E.Def {

    public class MoveItemDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__MoveItem_Done;

        // WM_Inventory::PostCEvt_MoveItem_Done
        public InvMoveDesc _iDesc;

        public MoveItemDoneCEvt() {

        }

        public MoveItemDoneCEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvMoveDesc>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iDesc);
        }
    }
}
