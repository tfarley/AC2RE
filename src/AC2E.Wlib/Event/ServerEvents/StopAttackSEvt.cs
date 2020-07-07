using System.IO;

namespace AC2E.WLib {

    public class StopAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__StopAttack;

        // WM_Combat::SendSEvt_StopAttack

        public StopAttackSEvt(BinaryReader data) {

        }
    }
}
