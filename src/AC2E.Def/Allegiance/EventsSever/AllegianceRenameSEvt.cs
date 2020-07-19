using System.IO;

namespace AC2E.Def {

    public class AllegianceRenameSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceRename;

        // WM_Allegiance::SendSEvt_AllegianceRename
        public WPString _name;

        public AllegianceRenameSEvt(BinaryReader data) {
            _name = data.UnpackPackage<WPString>();
        }
    }
}
