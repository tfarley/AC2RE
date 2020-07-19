using System.IO;

namespace AC2E.Def {

    public class BreakAllegianceSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__BreakAllegiance;

        // WM_Allegiance::SendSEvt_BreakAllegiance
        public InstanceId _trg;

        public BreakAllegianceSEvt(BinaryReader data) {
            _trg = data.UnpackInstanceId();
        }
    }
}
