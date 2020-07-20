namespace AC2E.Def {

    public class StartAttackSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__StartAttack;

        // WM_Combat::SendSEvt_StartAttack
        public InstanceId _target;

        public StartAttackSEvt(AC2Reader data) {
            _target = data.UnpackInstanceId();
        }
    }
}
