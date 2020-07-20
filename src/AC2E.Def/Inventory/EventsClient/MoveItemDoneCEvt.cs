﻿namespace AC2E.Def {

    public class MoveItemDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__MoveItem_Done;

        // WM_Inventory::PostCEvt_MoveItem_Done
        public InvMoveDesc _iDesc;

        public MoveItemDoneCEvt() {

        }

        public MoveItemDoneCEvt(AC2Reader data) {
            _iDesc = data.UnpackPackage<InvMoveDesc>();
        }

        public void write(AC2Writer data) {
            data.Pack(_iDesc);
        }
    }
}
