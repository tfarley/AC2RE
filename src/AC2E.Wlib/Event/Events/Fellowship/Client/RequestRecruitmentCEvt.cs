using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RequestRecruitmentCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Fellowship__RequestRecruitment;

        // WM_Fellowship::PostCEvt_RequestRecruitment
        public StringInfo _fellowship_name;
        public StringInfo _leader_name;
        public InstanceId _leader;

        public RequestRecruitmentCEvt() {

        }

        public RequestRecruitmentCEvt(BinaryReader data) {
            _fellowship_name = data.UnpackPackage<StringInfo>();
            _leader_name = data.UnpackPackage<StringInfo>();
            _leader = data.UnpackInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Pack(_fellowship_name);
            data.Pack(_leader_name);
            data.Pack(_leader);
        }
    }
}
