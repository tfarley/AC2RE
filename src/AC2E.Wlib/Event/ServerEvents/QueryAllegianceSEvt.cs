using System.IO;

namespace AC2E.WLib {

    public class QueryAllegianceSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__QueryAllegiance;

        // WM_Allegiance::SendSEvt_QueryAllegiance

        public QueryAllegianceSEvt(BinaryReader data) {

        }
    }
}
