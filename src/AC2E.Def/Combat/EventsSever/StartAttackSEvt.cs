namespace AC2E.Def {

    public class StartAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__StartAttack;

        // WM_Combat::SendSEvt_StartAttack
        public InstanceId targetId; // _target

        public StartAttackSEvt(AC2Reader data) {
            targetId = data.UnpackInstanceId();
        }
    }
}
