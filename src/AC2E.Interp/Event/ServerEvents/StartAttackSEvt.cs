using System.IO;

namespace AC2E.Interp.Event.ServerEvents {

    public class StartAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__StartAttack;

        public StartAttackSEvt(BinaryReader data) {

        }
    }
}
