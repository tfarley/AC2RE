using System.IO;

namespace AC2E.WLib {

    public class TakeAllFromContainerDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Inventory__TakeAllFromContainer_Done;

        // WM_Inventory::PostCEvt_TakeAllFromContainer_Done
        public InvTakeAllDescPkg _iDesc;

        public TakeAllFromContainerDoneCEvt() {

        }

        public TakeAllFromContainerDoneCEvt(BinaryReader data) {
            _iDesc = data.UnpackPackage<InvTakeAllDescPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_iDesc);
        }
    }
}
