namespace AC2E.Def {

    public class EnterPortalSpaceCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__EnterPortalSpace;

        // WM_Player::PostCEvt_EnterPortalSpace
        public double _delay;

        public EnterPortalSpaceCEvt() {

        }

        public EnterPortalSpaceCEvt(AC2Reader data) {
            _delay = data.UnpackDouble();
        }

        public void write(AC2Writer data) {
            data.Pack(_delay);
        }
    }
}
