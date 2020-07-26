namespace AC2E.Def {

    public class ToggleSpecialAttackCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__ToggleSpecialAttack;

        // WM_Combat::PostCEvt_ToggleSpecialAttack
        public bool toggled; // _toggled
        public uint maneuver; // _maneuver

        public ToggleSpecialAttackCEvt() {

        }

        public ToggleSpecialAttackCEvt(AC2Reader data) {
            toggled = data.UnpackBoolean();
            maneuver = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(toggled);
            data.Pack(maneuver);
        }
    }
}
