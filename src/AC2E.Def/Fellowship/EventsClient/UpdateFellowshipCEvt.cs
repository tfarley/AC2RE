using System.IO;

namespace AC2E.Def {

    public class UpdateFellowshipCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowship;

        // WM_Fellowship::PostCEvt_UpdateFellowship
        public Fellowship _fellowship;

        public UpdateFellowshipCEvt() {

        }

        public UpdateFellowshipCEvt(BinaryReader data) {
            _fellowship = data.UnpackPackage<Fellowship>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_fellowship);
        }
    }
}
