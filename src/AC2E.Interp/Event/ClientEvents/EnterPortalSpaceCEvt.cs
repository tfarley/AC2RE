using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Interp.Event.ClientEvents {

    public class EnterPortalSpaceCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__EnterPortalSpace;

        // WM_Player::PostCEvt_EnterPortalSpace
        public double _delay;

        public EnterPortalSpaceCEvt() {

        }

        public EnterPortalSpaceCEvt(BinaryReader data) {
            _delay = data.UnpackDouble();
        }

        public void write(BinaryWriter data) {
            data.Pack(_delay);
        }
    }
}
