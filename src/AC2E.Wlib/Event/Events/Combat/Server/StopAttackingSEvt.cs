using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class StopAttackingSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__StopAttacking;

        // WM_Combat::SendSEvt_StopAttacking
        public InstanceId _target;

        public StopAttackingSEvt(BinaryReader data) {
            _target = data.UnpackInstanceId();
        }
    }
}
