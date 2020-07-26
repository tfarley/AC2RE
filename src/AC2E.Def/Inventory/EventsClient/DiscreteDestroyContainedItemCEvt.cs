namespace AC2E.Def {

    public class DiscreteDestroyContainedItemCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__DiscreteDestroyContainedItem;

        // WM_Inventory::PostCEvt_DiscreteDestroyContainedItem
        public InstanceId itemId; // _itemID

        public DiscreteDestroyContainedItemCEvt() {

        }

        public DiscreteDestroyContainedItemCEvt(AC2Reader data) {
            itemId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(itemId);
        }
    }
}
