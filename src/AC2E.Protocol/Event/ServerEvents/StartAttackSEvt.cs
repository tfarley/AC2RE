using System.IO;

namespace AC2E.Protocol.Event.ServerEvents {

    public class StartAttackSEvt : INetServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__StartAttack;

        public StartAttackSEvt(BinaryReader data) {

        }
    }
}
