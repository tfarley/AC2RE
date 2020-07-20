namespace AC2E.Def {

    public class DragFromMoneyBagDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Money__DragFromMoneyBag_Done;

        // WM_Money::PostCEvt_DragFromMoneyBag_Done
        public InstanceId _iidToContainer;

        public DragFromMoneyBagDoneCEvt() {

        }

        public DragFromMoneyBagDoneCEvt(AC2Reader data) {
            _iidToContainer = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_iidToContainer);
        }
    }
}
