using System.IO;

namespace AC2E.WLib {

    public class BasicAttacksFailedCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__Combat_BasicAttacksFailed;

        // WM_Combat::PostCEvt_Combat_BasicAttacksFailed
        public uint _err;

        public BasicAttacksFailedCEvt() {

        }

        public BasicAttacksFailedCEvt(BinaryReader data) {
            _err = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_err);
        }
    }
}
