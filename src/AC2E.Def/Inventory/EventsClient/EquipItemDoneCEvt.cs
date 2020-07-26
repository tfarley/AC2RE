namespace AC2E.Def {

    public class EquipItemDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__EquipItem_Done;

        // WM_Inventory::PostCEvt_EquipItem_Done
        public InvEquipDesc equipDesc; // _eDesc

        public EquipItemDoneCEvt() {

        }

        public EquipItemDoneCEvt(AC2Reader data) {
            equipDesc = data.UnpackPackage<InvEquipDesc>();
        }

        public void write(AC2Writer data) {
            data.Pack(equipDesc);
        }
    }
}
