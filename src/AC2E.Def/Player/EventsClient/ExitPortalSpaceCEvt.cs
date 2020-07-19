using System.IO;

namespace AC2E.Def {

    public class ExitPortalSpaceCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ExitPortalSpace;

        // WM_Player::PostCEvt_ExitPortalSpace
        public double _delay;

        public ExitPortalSpaceCEvt() {

        }

        public ExitPortalSpaceCEvt(BinaryReader data) {
            _delay = data.UnpackDouble();
        }

        public void write(BinaryWriter data) {
            data.Pack(_delay);
        }
    }
}
