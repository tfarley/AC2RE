namespace AC2E.Def {

    public class ToggleSpecialAttackCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__ToggleSpecialAttack;

        // WM_Combat::PostCEvt_ToggleSpecialAttack
        public bool _toggled;
        public uint _maneuver;

        public ToggleSpecialAttackCEvt() {

        }

        public ToggleSpecialAttackCEvt(AC2Reader data) {
            _toggled = data.UnpackBoolean();
            _maneuver = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_toggled);
            data.Pack(_maneuver);
        }
    }
}
