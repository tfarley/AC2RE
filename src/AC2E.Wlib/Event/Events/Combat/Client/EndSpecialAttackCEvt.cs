﻿using System.IO;

namespace AC2E.WLib {

    public class EndSpecialAttackCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__EndSpecialAttack;

        // WM_Combat::PostCEvt_EndSpecialAttack
        public uint _last_special_attack_id;

        public EndSpecialAttackCEvt() {

        }

        public EndSpecialAttackCEvt(BinaryReader data) {
            _last_special_attack_id = data.UnpackUInt32();
        }

        public void write(BinaryWriter data) {
            data.Pack(_last_special_attack_id);
        }
    }
}
