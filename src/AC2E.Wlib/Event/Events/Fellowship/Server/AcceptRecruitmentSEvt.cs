using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class AcceptRecruitmentSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Fellowship__AcceptRecruitment;

        // WM_Fellowship::SendSEvt_AcceptRecruitment
        public InstanceId _leader;

        public AcceptRecruitmentSEvt(BinaryReader data) {
            _leader = data.UnpackInstanceId();
        }
    }
}
