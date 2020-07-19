using System.IO;

namespace AC2E.Def {

    public class TryToUseItemDoneCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Usage__TryToUseItem_Done;

        // WM_Usage::PostCEvt_Usage_TryToUseItem_Done
        public UsageDesc _uDesc;

        public TryToUseItemDoneCEvt() {

        }

        public TryToUseItemDoneCEvt(BinaryReader data) {
            _uDesc = data.UnpackPackage<UsageDesc>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_uDesc);
        }
    }
}
