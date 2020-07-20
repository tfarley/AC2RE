namespace AC2E.Def {

    public class BasicAttacksFailedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__Combat_BasicAttacksFailed;

        // WM_Combat::PostCEvt_Combat_BasicAttacksFailed
        public uint _err;

        public BasicAttacksFailedCEvt() {

        }

        public BasicAttacksFailedCEvt(AC2Reader data) {
            _err = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_err);
        }
    }
}
