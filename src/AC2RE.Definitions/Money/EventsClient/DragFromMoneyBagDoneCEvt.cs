namespace AC2RE.Definitions {

    public class DragFromMoneyBagDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Money__DragFromMoneyBag_Done;

        // WM_Money::PostCEvt_DragFromMoneyBag_Done
        public InstanceId toContainerId; // _iidToContainer

        public DragFromMoneyBagDoneCEvt() {

        }

        public DragFromMoneyBagDoneCEvt(AC2Reader data) {
            toContainerId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(toContainerId);
        }
    }
}
