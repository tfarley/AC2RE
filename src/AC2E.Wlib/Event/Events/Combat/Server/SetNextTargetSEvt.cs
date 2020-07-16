using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class SetNextTargetSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__SetNextTarget;

        // WM_Combat::SendSEvt_SetNextTarget
        public InstanceId _target;

        public SetNextTargetSEvt(BinaryReader data) {
            _target = data.UnpackInstanceId();
        }
    }
}
