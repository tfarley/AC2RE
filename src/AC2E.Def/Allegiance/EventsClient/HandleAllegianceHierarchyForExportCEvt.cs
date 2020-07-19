using System.IO;

namespace AC2E.Def {

    public class HandleAllegianceHierarchyForExportCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__HandleAllegianceHierarchyForExport;

        // WM_Allegiance::PostCEvt_HandleAllegianceHierarchyForExport
        public WPString _filename;
        public AllegianceHierarchy _hier;

        public HandleAllegianceHierarchyForExportCEvt() {

        }

        public HandleAllegianceHierarchyForExportCEvt(BinaryReader data) {
            _filename = data.UnpackPackage<WPString>();
            _hier = data.UnpackPackage<AllegianceHierarchy>();
        }

        public void write(BinaryWriter data) {
            data.Pack(_filename);
            data.Pack(_hier);
        }
    }
}
