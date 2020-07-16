using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class UploadUISettingsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__UploadUISettings;

        // WM_Player::SendSEvt_UploadUISettings
        public AAHash _opacities;
        public UISaveLocations _locations;

        public UploadUISettingsSEvt(BinaryReader data) {
            _opacities = data.UnpackPackage<AAHash>();
            _locations = data.UnpackPackage<UISaveLocations>();
        }
    }
}
