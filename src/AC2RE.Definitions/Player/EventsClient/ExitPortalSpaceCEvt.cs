namespace AC2RE.Definitions {

    public class ExitPortalSpaceCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ExitPortalSpace;

        // WM_Player::PostCEvt_ExitPortalSpace
        public double delay; // _delay

        public ExitPortalSpaceCEvt() {

        }

        public ExitPortalSpaceCEvt(AC2Reader data) {
            delay = data.UnpackDouble();
        }

        public void write(AC2Writer data) {
            data.Pack(delay);
        }
    }
}
