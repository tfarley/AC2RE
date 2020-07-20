namespace AC2E.Def {

    public class DestroyContainedItemCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__DestroyContainedItem;

        // WM_Inventory::PostCEvt_DestroyContainedItem
        public InstanceId _itemID;
        public InstanceId _containerID;

        public DestroyContainedItemCEvt() {

        }

        public DestroyContainedItemCEvt(AC2Reader data) {
            _itemID = data.UnpackInstanceId();
            _containerID = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_itemID);
            data.Pack(_containerID);
        }
    }
}
