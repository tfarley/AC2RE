using System.IO;

namespace AC2E.WLib {

    public class UpdateDeathStateCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Death__UpdateDeathState;

        // WM_Combat::PostCEvt_UpdateAttackState
        public bool _dead;

        public UpdateDeathStateCEvt() {

        }

        public UpdateDeathStateCEvt(BinaryReader data) {
            _dead = data.UnpackUInt32() != 0;
        }

        public void write(BinaryWriter data) {
            data.Pack(_dead ? (uint)1 : (uint)0);
        }
    }
}
