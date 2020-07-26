namespace AC2E.Def {

    public class UploadUISettingsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__UploadUISettings;

        // WM_Player::SendSEvt_UploadUISettings
        public AAHash opacities; // _opacities
        public UISaveLocations locations; // _locations

        public UploadUISettingsSEvt(AC2Reader data) {
            opacities = data.UnpackPackage<AAHash>();
            locations = data.UnpackPackage<UISaveLocations>();
        }
    }
}
