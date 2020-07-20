namespace AC2E.Def {

    public class ExitPortalSpaceCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ExitPortalSpace;

        // WM_Player::PostCEvt_ExitPortalSpace
        public double _delay;

        public ExitPortalSpaceCEvt() {

        }

        public ExitPortalSpaceCEvt(AC2Reader data) {
            _delay = data.UnpackDouble();
        }

        public void write(AC2Writer data) {
            data.Pack(_delay);
        }
    }
}
