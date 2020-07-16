using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class QueryAllegianceProfileSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__QueryAllegianceProfile;

        // WM_Allegiance::SendSEvt_QueryAllegianceProfile
        public InstanceId _trg;

        public QueryAllegianceProfileSEvt(BinaryReader data) {
            _trg = data.UnpackInstanceId();
        }
    }
}
