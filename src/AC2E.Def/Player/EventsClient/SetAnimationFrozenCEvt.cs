namespace AC2E.Def {

    public class SetAnimationFrozenCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__SetAnimationFrozen;

        // WM_Player::PostCEvt_SetAnimationFrozen
        public bool _bFrozen;

        public SetAnimationFrozenCEvt() {

        }

        public SetAnimationFrozenCEvt(AC2Reader data) {
            _bFrozen = data.UnpackUInt32() != 0;
        }

        public void write(AC2Writer data) {
            data.Pack(_bFrozen ? (uint)1 : (uint)0);
        }
    }
}
