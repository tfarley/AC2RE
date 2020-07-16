using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class RecruitFellowSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Fellowship__RecruitFellow;

        // WM_Fellowship::SendSEvt_RecruitFellow
        public InstanceId _fellow;

        public RecruitFellowSEvt(BinaryReader data) {
            _fellow = data.UnpackInstanceId();
        }
    }
}
