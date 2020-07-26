namespace AC2E.Def {

    public class AttackErrorCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__Combat_AttackError;

        // WM_Combat::PostCEvt_Combat_AttackError
        public uint skill; // _skill
        public InstanceId targetId; // _iidTarget
        public uint error; // _err

        public AttackErrorCEvt() {

        }

        public AttackErrorCEvt(AC2Reader data) {
            skill = data.UnpackUInt32();
            targetId = data.UnpackInstanceId();
            error = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(skill);
            data.Pack(targetId);
            data.Pack(error);
        }
    }
}
