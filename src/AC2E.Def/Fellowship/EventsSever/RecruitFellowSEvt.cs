namespace AC2E.Def {

    public class RecruitFellowSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Fellowship__RecruitFellow;

        // WM_Fellowship::SendSEvt_RecruitFellow
        public InstanceId _fellow;

        public RecruitFellowSEvt(AC2Reader data) {
            _fellow = data.UnpackInstanceId();
        }
    }
}
