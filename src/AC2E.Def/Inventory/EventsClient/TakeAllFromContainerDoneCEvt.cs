using System.IO;

namespace AC2E.Def {

    public class TakeAllFromContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__TakeAllFromContainer_Done;

        // WM_Inventory::PostCEvt_TakeAllFromContainer_Done
        public InvTakeAllDesc _iDesc;

        public TakeAllFromContainerDoneCEvt() {

        }

        public TakeAllFromContainerDoneCEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvTakeAllDesc>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iDesc);
        }
    }
}
