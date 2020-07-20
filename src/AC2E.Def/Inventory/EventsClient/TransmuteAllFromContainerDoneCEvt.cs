namespace AC2E.Def {

    public class TransmuteAllFromContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__TransmuteAllFromContainer_Done;

        // WM_Inventory::PostCEvt_TransmuteAllFromContainer_Done
        public InvTransmuteAllDesc _iDesc;

        public TransmuteAllFromContainerDoneCEvt() {

        }

        public TransmuteAllFromContainerDoneCEvt(AC2Reader data) {
            _iDesc = data.UnpackPackage<InvTransmuteAllDesc>();
        }

        public void write(AC2Writer data) {
            data.Pack(_iDesc);
        }
    }
}
