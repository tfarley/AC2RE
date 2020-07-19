using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class ExpireRecruitmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__ExpireRecruitment;

        // WM_Fellowship::PostCEvt_ExpireRecruitment
        public InstanceId _leader;

        public ExpireRecruitmentCEvt() {

        }

        public ExpireRecruitmentCEvt(BinaryReader data) {
            _leader = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_leader);
        }
    }
}
