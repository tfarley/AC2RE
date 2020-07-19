using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class AllegianceTreeExportSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceTreeExport;

        // WM_Allegiance::SendSEvt_AllegianceTreeExport
        public WPString _filename;

        public AllegianceTreeExportSEvt(BinaryReader data) {
            _filename = data.UnpackPackage<WPString>();
        }
    }
}
