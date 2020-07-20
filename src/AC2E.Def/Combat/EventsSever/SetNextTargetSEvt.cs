namespace AC2E.Def {

    public class SetNextTargetSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__SetNextTarget;

        // WM_Combat::SendSEvt_SetNextTarget
        public InstanceId _target;

        public SetNextTargetSEvt(AC2Reader data) {
            _target = data.UnpackInstanceId();
        }
    }
}
