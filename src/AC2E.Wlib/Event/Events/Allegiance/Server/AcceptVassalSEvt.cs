using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class AcceptVassalSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AcceptVassal;

        // WM_Allegiance::SendSEvt_AcceptVassal
        public InstanceId _vassal;

        public AcceptVassalSEvt(BinaryReader data) {
            _vassal = data.UnpackInstanceId();
        }
    }
}
