using System.IO;

namespace AC2E.Def {

    public class RecruitFellowSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Fellowship__RecruitFellow;

        // WM_Fellowship::SendSEvt_RecruitFellow
        public InstanceId _fellow;

        public RecruitFellowSEvt(BinaryReader data) {
            _fellow = data.UnpackInstanceId();
        }
    }
}
