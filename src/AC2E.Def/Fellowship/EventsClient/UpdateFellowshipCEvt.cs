namespace AC2E.Def {

    public class UpdateFellowshipCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__UpdateFellowship;

        // WM_Fellowship::PostCEvt_UpdateFellowship
        public Fellowship _fellowship;

        public UpdateFellowshipCEvt() {

        }

        public UpdateFellowshipCEvt(AC2Reader data) {
            _fellowship = data.UnpackPackage<Fellowship>();
        }

        public void write(AC2Writer data) {
            data.Pack(_fellowship);
        }
    }
}
