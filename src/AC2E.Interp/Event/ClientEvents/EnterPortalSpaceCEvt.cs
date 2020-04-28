using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Interp.Event.ClientEvents {

    public class EnterPortalSpaceCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__EnterPortalSpace;

        public double delay;

        public void write(BinaryWriter data) {
            data.Pack(delay);
        }
    }
}
