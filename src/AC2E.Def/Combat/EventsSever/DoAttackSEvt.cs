namespace AC2E.Def {

    public class DoAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__DoAttack;

        // WM_Combat::SendSEvt_DoAttack
        public uint _special_attack_id;
        public uint _maneuver;
        public InstanceId _target;

        public DoAttackSEvt(AC2Reader data) {
            _special_attack_id = data.UnpackUInt32();
            _maneuver = data.UnpackUInt32();
            _target = data.UnpackInstanceId();
        }
    }
}
