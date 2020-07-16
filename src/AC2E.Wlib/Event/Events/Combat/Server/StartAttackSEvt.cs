using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class StartAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__StartAttack;

        // WM_Combat::SendSEvt_StartAttack
        public InstanceId _target;

        public StartAttackSEvt(BinaryReader data) {
            _target = data.UnpackInstanceId();
        }
    }
}
