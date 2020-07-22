﻿namespace AC2E.Def {

    public class UploadUISettingsSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__UploadUISettings;

        // WM_Player::SendSEvt_UploadUISettings
        public AAHash _opacities;
        public UISaveLocations _locations;

        public UploadUISettingsSEvt(AC2Reader data) {
            _opacities = data.UnpackPackage<AAHash>();
            _locations = data.UnpackPackage<UISaveLocations>();
        }
    }
}