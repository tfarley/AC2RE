namespace AC2E.Def {

    public class UpdateDeathStateCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__UpdateDeathState;

        // WM_Combat::PostCEvt_UpdateAttackState
        public bool _dead;

        public UpdateDeathStateCEvt() {

        }

        public UpdateDeathStateCEvt(AC2Reader data) {
            _dead = data.UnpackBoolean();
        }

        public void write(AC2Writer data) {
            data.Pack(_dead);
        }
    }
}
