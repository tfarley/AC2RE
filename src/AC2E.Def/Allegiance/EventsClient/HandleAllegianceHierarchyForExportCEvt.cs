namespace AC2E.Def {

    public class HandleAllegianceHierarchyForExportCEvt : IClientEvent {

        public ClientEventFunctionId funcId => ClientEventFunctionId.Allegiance__HandleAllegianceHierarchyForExport;

        // WM_Allegiance::PostCEvt_HandleAllegianceHierarchyForExport
        public WPString fileName; // _filename
        public AllegianceHierarchy hierarchy; // _hier

        public HandleAllegianceHierarchyForExportCEvt() {

        }

        public HandleAllegianceHierarchyForExportCEvt(AC2Reader data) {
            fileName = data.UnpackPackage<WPString>();
            hierarchy = data.UnpackPackage<AllegianceHierarchy>();
        }

        public void write(AC2Writer data) {
            data.Pack(fileName);
            data.Pack(hierarchy);
        }
    }
}
