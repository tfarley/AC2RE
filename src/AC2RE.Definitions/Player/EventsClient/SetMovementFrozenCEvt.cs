namespace AC2RE.Definitions {

    public class SetMovementFrozenCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__SetMovementFrozen;

        // WM_Player::PostCEvt_SetMovementFrozen
        public bool frozen; // _bFrozen

        public SetMovementFrozenCEvt() {

        }

        public SetMovementFrozenCEvt(AC2Reader data) {
            frozen = data.UnpackBoolean();
        }

        public void write(AC2Writer data) {
            data.Pack(frozen);
        }
    }
}
