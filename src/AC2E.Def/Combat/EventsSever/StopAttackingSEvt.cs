namespace AC2E.Def {

    public class StopAttackingSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__StopAttacking;

        // WM_Combat::SendSEvt_StopAttacking
        public InstanceId targetId; // _target

        public StopAttackingSEvt(AC2Reader data) {
            targetId = data.UnpackInstanceId();
        }
    }
}
