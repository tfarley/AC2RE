namespace AC2RE.Definitions {

    public class ExpireRecruitmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__ExpireRecruitment;

        // WM_Fellowship::PostCEvt_ExpireRecruitment
        public InstanceId leaderId; // _leader

        public ExpireRecruitmentCEvt() {

        }

        public ExpireRecruitmentCEvt(AC2Reader data) {
            leaderId = data.UnpackInstanceId();
        }

        public void write(AC2Writer data) {
            data.Pack(leaderId);
        }
    }
}
