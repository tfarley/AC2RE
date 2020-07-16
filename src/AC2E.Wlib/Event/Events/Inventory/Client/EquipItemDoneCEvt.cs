using System.IO;

namespace AC2E.WLib {

    public class EquipItemDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__EquipItem_Done;

        // WM_Inventory::PostCEvt_EquipItem_Done
        public InvEquipDescPkg _eDesc;

        public EquipItemDoneCEvt() {

        }

        public EquipItemDoneCEvt(BinaryReader data) {
            _eDesc = data.UnpackPackage<InvEquipDescPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_eDesc);
        }
    }
}
