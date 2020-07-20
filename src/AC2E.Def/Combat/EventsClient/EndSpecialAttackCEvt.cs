namespace AC2E.Def {

    public class EndSpecialAttackCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Combat__EndSpecialAttack;

        // WM_Combat::PostCEvt_EndSpecialAttack
        public uint _last_special_attack_id;

        public EndSpecialAttackCEvt() {

        }

        public EndSpecialAttackCEvt(AC2Reader data) {
            _last_special_attack_id = data.UnpackUInt32();
        }

        public void write(AC2Writer data) {
            data.Pack(_last_special_attack_id);
        }
    }
}
