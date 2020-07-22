﻿namespace AC2E.Def {

    public class ClearMarkersCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Player__ClearMarkers;

        // WM_Player::RecvCEvt_ClearMarkers
        public uint type;

        public ClearMarkersCEvt() {

        }

        public ClearMarkersCEvt(AC2Reader data) {
            type = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(type);
        }
    }
}