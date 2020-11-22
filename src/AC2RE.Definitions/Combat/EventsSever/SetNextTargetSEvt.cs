namespace AC2RE.Definitions {

    public class SetNextTargetSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Combat__SetNextTarget;

        // WM_Combat::SendSEvt_SetNextTarget
        public InstanceId targetId; // _target

        public SetNextTargetSEvt(AC2Reader data) {
            targetId = data.UnpackInstanceId();
        }
    }
}
