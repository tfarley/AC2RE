namespace AC2E.Def {

    public class RequestRecruitmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__RequestRecruitment;

        // WM_Fellowship::PostCEvt_RequestRecruitment
        public StringInfo _fellowship_name;
        public StringInfo _leader_name;
        public InstanceId _leader;

        public RequestRecruitmentCEvt() {

        }

        public RequestRecruitmentCEvt(AC2Reader data) {
            _fellowship_name = data.UnpackPackage<StringInfo>();
            _leader_name = data.UnpackPackage<StringInfo>();
            _leader = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(_fellowship_name);
            data.Pack(_leader_name);
            data.Pack(_leader);
        }
    }
}
