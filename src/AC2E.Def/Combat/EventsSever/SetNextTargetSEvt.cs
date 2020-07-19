using System.IO;

namespace AC2E.Def {

    public class SetNextTargetSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__SetNextTarget;

        // WM_Combat::SendSEvt_SetNextTarget
        public InstanceId _target;

        public SetNextTargetSEvt(BinaryReader data) {
            _target = data.UnpackInstanceId();
        }
    }
}
