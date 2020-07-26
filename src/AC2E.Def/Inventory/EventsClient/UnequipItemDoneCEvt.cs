﻿namespace AC2E.Def {

    public class UnequipItemDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__UnEquipItem_Done;

        // CPlayer::RecvCEvt_UnEquipItem_Done
        public InvEquipDesc equipDesc; // _eDesc

        public UnequipItemDoneCEvt() {

        }

        public UnequipItemDoneCEvt(AC2Reader data) {
            equipDesc = data.UnpackPackage<InvEquipDesc>();
        }

        public void write(AC2Writer data) {
            data.Pack(equipDesc);
        }
    }
}
