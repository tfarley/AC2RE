namespace AC2E.Def {

    public class SetMovementFrozenCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__SetMovementFrozen;

        // WM_Player::PostCEvt_SetMovementFrozen
        public bool _bFrozen;

        public SetMovementFrozenCEvt() {

        }

        public SetMovementFrozenCEvt(AC2Reader data) {
            _bFrozen = data.UnpackBoolean();
        }

        public void write(AC2Writer data) {
            data.Pack(_bFrozen);
        }
    }
}
