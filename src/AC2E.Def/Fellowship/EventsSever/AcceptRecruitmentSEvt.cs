namespace AC2E.Def {

    public class AcceptRecruitmentSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Fellowship__AcceptRecruitment;

        // WM_Fellowship::SendSEvt_AcceptRecruitment
        public InstanceId _leader;

        public AcceptRecruitmentSEvt(AC2Reader data) {
            _leader = data.UnpackInstanceId();
        }
    }
}
