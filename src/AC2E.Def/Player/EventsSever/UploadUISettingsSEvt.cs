using System.Collections.Generic;

namespace AC2E.Def {

    public class UploadUISettingsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__UploadUISettings;

        // WM_Player::SendSEvt_UploadUISettings
        public Dictionary<uint, uint> opacities; // _opacities
        public UISaveLocations locations; // _locations

        public UploadUISettingsSEvt(AC2Reader data) {
            opacities = data.UnpackPackage<AAHash>();
            locations = data.UnpackPackage<UISaveLocations>();
        }
    }
}
