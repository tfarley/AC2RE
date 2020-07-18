using System.IO;

namespace AC2E.WLib {

    public class UpdateFellowshipCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowship;

        // WM_Fellowship::PostCEvt_UpdateFellowship
        public FellowshipPkg _fellowship;

        public UpdateFellowshipCEvt() {

        }

        public UpdateFellowshipCEvt(BinaryReader data) {
            _fellowship = data.UnpackPackage<FellowshipPkg>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_fellowship);
        }
    }
}
