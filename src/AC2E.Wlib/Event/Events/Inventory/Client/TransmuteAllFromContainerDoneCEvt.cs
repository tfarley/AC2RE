using System.IO;

namespace AC2E.WLib {

    public class TransmuteAllFromContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__TransmuteAllFromContainer_Done;

        // WM_Inventory::PostCEvt_TransmuteAllFromContainer_Done
        public InvTransmuteAllDescPkg _iDesc;

        public TransmuteAllFromContainerDoneCEvt() {

        }

        public TransmuteAllFromContainerDoneCEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvTransmuteAllDescPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iDesc);
        }
    }
}
