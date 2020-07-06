﻿using System.IO;

namespace AC2E.WLib {

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