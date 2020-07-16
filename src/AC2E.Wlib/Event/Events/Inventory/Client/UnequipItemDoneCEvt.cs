using System.IO;

namespace AC2E.WLib {

    public class UnequipItemDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__UnEquipItem_Done;

        // CPlayer::RecvCEvt_UnEquipItem_Done
        public InvEquipDescPkg _eDesc;

        public UnequipItemDoneCEvt() {

        }

        public UnequipItemDoneCEvt(BinaryReader data) {
            _eDesc = data.UnpackPackage<InvEquipDescPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_eDesc);
        }
    }
}
