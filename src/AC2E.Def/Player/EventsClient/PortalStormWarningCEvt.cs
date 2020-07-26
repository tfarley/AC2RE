namespace AC2E.Def {

    public class PortalStormWarningCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__PortalStorm_Warning;

        // WM_Player::PostCEvt_PortalStorm_Warning
        public float intensity; // __intensity
        public CellId cell; // __cellID

        public PortalStormWarningCEvt() {

        }

        public PortalStormWarningCEvt(AC2Reader data) {
            intensity = data.UnpackSingle();
            cell = new CellId(data.UnpackUInt32());
        }

        public void write(AC2Writer data) {
            data.Pack(intensity);
            data.Pack(cell.id);
        }
    }
}
