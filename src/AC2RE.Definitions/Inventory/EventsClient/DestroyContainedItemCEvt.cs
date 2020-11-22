namespace AC2RE.Definitions {

    public class DestroyContainedItemCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__DestroyContainedItem;

        // WM_Inventory::PostCEvt_DestroyContainedItem
        public InstanceId itemId; // _itemID
        public InstanceId containerId; // _containerID

        public DestroyContainedItemCEvt() {

        }

        public DestroyContainedItemCEvt(AC2Reader data) {
            itemId = data.UnpackInstanceId();
            containerId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(itemId);
            data.Pack(containerId);
        }
    }
}
