using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Protocol.Event.ClientEvents {

    public class EnterPortalSpaceCEvt : INetClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__EnterPortalSpace;

        public double delay;

        public void write(BinaryWriter data) {
            data.Pack(delay);
        }
    }
}
