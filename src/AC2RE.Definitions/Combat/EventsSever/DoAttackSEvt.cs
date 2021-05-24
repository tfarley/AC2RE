namespace AC2RE.Definitions {

    public class DoAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__DoAttack;

        // WM_Combat::SendSEvt_DoAttack
        public uint specialAttackId; // _special_attack_id
        public SkillId skillId; // _maneuver
        public InstanceId targetId; // _target

        public DoAttackSEvt(AC2Reader data) {
            specialAttackId = data.UnpackUInt32();
            skillId = (SkillId)data.UnpackUInt32();
            targetId = data.UnpackInstanceId();
        }
    }
}
