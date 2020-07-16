using System.IO;

namespace AC2E.WLib {

    public class TryToUseItemDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Usage__TryToUseItem_Done;

        // WM_Usage::PostCEvt_Usage_TryToUseItem_Done
        public UsageDescPkg _uDesc;

        public TryToUseItemDoneCEvt() {

        }

        public TryToUseItemDoneCEvt(BinaryReader data) {
            _uDesc = data.UnpackPackage<UsageDescPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_uDesc);
        }
    }
}
