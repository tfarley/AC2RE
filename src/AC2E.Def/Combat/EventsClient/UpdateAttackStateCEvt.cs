namespace AC2E.Def {

    public class UpdateAttackStateCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__UpdateAttackState;

        // WM_Combat::PostCEvt_UpdateAttackState
        public bool _attacking;

        public UpdateAttackStateCEvt() {

        }

        public UpdateAttackStateCEvt(AC2Reader data) {
            _attacking = data.UnpackUInt32() != 0;
        }

        public void write(AC2Writer data) {
            data.Pack(_attacking ? (uint)1 : (uint)0);
        }
    }
}
