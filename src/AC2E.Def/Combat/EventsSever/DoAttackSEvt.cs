namespace AC2E.Def {

    public class DoAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__DoAttack;

        // WM_Combat::SendSEvt_DoAttack
        public uint specialAttackId; // _special_attack_id
        public uint maneuver; // _maneuver
        public InstanceId targetId; // _target

        public DoAttackSEvt(AC2Reader data) {
            specialAttackId = data.UnpackUInt32();
            maneuver = data.UnpackUInt32();
            targetId = data.UnpackInstanceId();
        }
    }
}
