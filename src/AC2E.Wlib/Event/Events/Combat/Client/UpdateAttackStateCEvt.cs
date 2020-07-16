using System.IO;

namespace AC2E.WLib {

    public class UpdateAttackStateCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__UpdateAttackState;

        // WM_Combat::PostCEvt_UpdateAttackState
        public bool _attacking;

        public UpdateAttackStateCEvt() {

        }

        public UpdateAttackStateCEvt(BinaryReader data) {
            _attacking = data.UnpackUInt32() != 0;
        }

        public void write(BinaryWriter data) {
            data.Pack(_attacking ? (uint)1 : (uint)0);
        }
    }
}
