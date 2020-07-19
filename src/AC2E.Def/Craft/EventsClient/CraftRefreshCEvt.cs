using System.IO;

namespace AC2E.Def {

    public class CraftRefreshCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__CraftRefresh;

        // WM_Craft::PostCEvt_CraftRefresh
        public CraftRegistry _craftReg;

        public CraftRefreshCEvt() {

        }

        public CraftRefreshCEvt(BinaryReader data) {
            _craftReg = data.UnpackPackage<CraftRegistry>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_craftReg);
        }
    }
}
