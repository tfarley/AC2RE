namespace AC2E.Def {

    public class DragToMoneyBagSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Money__DragToMoneyBag;

        // WM_Money::SendSEvt_DragToMoneyBag
        public InstanceId fromContainerId; // _iidFromContainer
        public uint fromSlot; // _fromSlot
        public uint quantity; // _quantity
        public InstanceId itemId; // _iidItem

        public DragToMoneyBagSEvt(AC2Reader data) {
            fromContainerId = data.UnpackInstanceId();
            fromSlot = data.UnpackUInt32();
            quantity = data.UnpackUInt32();
            itemId = data.UnpackInstanceId();
        }
    }
}
