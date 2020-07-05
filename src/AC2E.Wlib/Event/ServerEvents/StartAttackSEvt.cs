using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class StartAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__StartAttack;

        // WM_Combat::SendSEvt_StartAttack
        public InstanceId unk1;
        public InstanceId _target;
        // TODO: More here

        public StartAttackSEvt(BinaryReader data) {
            unk1 = data.ReadInstanceId();
            _target = data.ReadInstanceId();
        }
    }
}
