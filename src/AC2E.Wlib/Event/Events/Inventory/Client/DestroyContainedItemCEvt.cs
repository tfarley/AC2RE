using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class DestroyContainedItemCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__DestroyContainedItem;

        // WM_Inventory::PostCEvt_DestroyContainedItem
        public InstanceId _itemID;
        public InstanceId _containerID;

        public DestroyContainedItemCEvt() {

        }

        public DestroyContainedItemCEvt(BinaryReader data) {
            _itemID = data.UnpackInstanceId();
            _containerID = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_itemID);
            data.Pack(_containerID);
        }
    }
}
