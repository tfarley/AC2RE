using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class AllegianceMemberSearchSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceMemberSearch;

        // WM_Allegiance::SendSEvt_AllegianceMemberSearch
        public WPString _member;

        public AllegianceMemberSearchSEvt(BinaryReader data) {
            _member = data.UnpackPackage<WPString>();
        }
    }
}
