using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Interp.Event.ClientEvents {

    public class ExitPortalSpaceCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ExitPortalSpace;

        public double delay;

        public ExitPortalSpaceCEvt() {

        }

        public ExitPortalSpaceCEvt(BinaryReader data) {
            delay = data.UnpackDouble();
        }

        public void write(BinaryWriter data) {
            data.Pack(delay);
        }
    }
}
