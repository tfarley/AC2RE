using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Protocol.Event.ClientEvents {

    public class ExitPortalSpaceCEvt : INetClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ExitPortalSpace;

        public double delay;

        public void write(BinaryWriter data) {
            data.Pack(delay);
        }
    }
}
