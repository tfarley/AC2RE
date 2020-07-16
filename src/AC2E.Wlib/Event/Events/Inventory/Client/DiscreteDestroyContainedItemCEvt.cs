using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class DiscreteDestroyContainedItemCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__DiscreteDestroyContainedItem;

        // WM_Inventory::PostCEvt_DiscreteDestroyContainedItem
        public InstanceId _itemID;

        public DiscreteDestroyContainedItemCEvt() {

        }

        public DiscreteDestroyContainedItemCEvt(BinaryReader data) {
            _itemID = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_itemID);
        }
    }
}
