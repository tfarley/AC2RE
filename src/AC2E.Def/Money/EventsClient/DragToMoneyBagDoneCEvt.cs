namespace AC2E.Def {

    public class DragToMoneyBagDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Money__DragToMoneyBag_Done;

        // WM_Money::PostCEvt_DragToMoneyBag_Done
        public InstanceId _iidFromContainer;
        public uint _fromSlot;
        public InstanceId _iidItem;

        public DragToMoneyBagDoneCEvt() {

        }

        public DragToMoneyBagDoneCEvt(AC2Reader data) {
            _iidFromContainer = data.UnpackInstanceId();
            _fromSlot = data.UnpackUInt32();
            _iidItem = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_iidFromContainer);
            data.Pack(_fromSlot);
            data.Pack(_iidItem);
        }
    }
}
