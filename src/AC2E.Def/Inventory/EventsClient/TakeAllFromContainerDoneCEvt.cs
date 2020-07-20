namespace AC2E.Def {

    public class TakeAllFromContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__TakeAllFromContainer_Done;

        // WM_Inventory::PostCEvt_TakeAllFromContainer_Done
        public InvTakeAllDesc _iDesc;

        public TakeAllFromContainerDoneCEvt() {

        }

        public TakeAllFromContainerDoneCEvt(AC2Reader data) {
            _iDesc = data.UnpackPackage<InvTakeAllDesc>();
        }

        public void write(AC2Writer data) {
            data.Pack(_iDesc);
        }
    }
}
