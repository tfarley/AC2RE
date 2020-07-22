﻿namespace AC2E.Def {

    public class RequestPetModeSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Player__RequestPetMode;

        // WM_Player::SendSEvt_RequestPetAttack
        public uint _mode;
        public InstanceId _iidPet;

        public RequestPetModeSEvt(AC2Reader data) {
            _mode = data.UnpackUInt32();
            _iidPet = data.UnpackInstanceId();
        }
    }
}