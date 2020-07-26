namespace AC2E.Def {

    public class AllegianceTreeExportSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceTreeExport;

        // WM_Allegiance::SendSEvt_AllegianceTreeExport
        public WPString fileName; // _filename

        public AllegianceTreeExportSEvt(AC2Reader data) {
            fileName = data.UnpackPackage<WPString>();
        }
    }
}
