using System.IO;

namespace AC2E.WLib {

    public class CraftRefreshCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__CraftRefresh;

        // WM_Craft::PostCEvt_CraftRefresh
        public CraftRegistryPkg _craftReg;

        public CraftRefreshCEvt() {

        }

        public CraftRefreshCEvt(BinaryReader data) {
            _craftReg = data.UnpackPackage<CraftRegistryPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_craftReg);
        }
    }
}
