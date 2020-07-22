﻿namespace AC2E.Def {

    public class AttackErrorCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__Combat_AttackError;

        // WM_Combat::PostCEvt_Combat_AttackError
        public uint _skill;
        public InstanceId _iidTarget;
        public uint _err;

        public AttackErrorCEvt() {

        }

        public AttackErrorCEvt(AC2Reader data) {
            _skill = data.UnpackUInt32();
            _iidTarget = data.UnpackInstanceId();
            _err = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_skill);
            data.Pack(_iidTarget);
            data.Pack(_err);
        }
    }
}