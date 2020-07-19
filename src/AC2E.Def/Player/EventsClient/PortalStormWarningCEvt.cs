using System.IO;

namespace AC2E.Def {

    public class PortalStormWarningCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__PortalStorm_Warning;

        // WM_Player::PostCEvt_PortalStorm_Warning
        public float __intensity;
        public CellId __cellID;

        public PortalStormWarningCEvt() {

        }

        public PortalStormWarningCEvt(BinaryReader data) {
            __intensity = data.UnpackSingle();
            __cellID = new CellId(data.UnpackUInt32());
        }

        public void write(BinaryWriter data) {
            data.Pack(__intensity);
            data.Pack(__cellID.id);
        }
    }
}
